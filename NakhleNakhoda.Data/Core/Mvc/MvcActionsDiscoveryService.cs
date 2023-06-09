﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NakhleNakhoda.Data.Core.Mvc
{
    /// <summary>
    /// MvcActions Discovery Service
    /// </summary>
    public class MvcActionsDiscoveryService : IMvcActionsDiscoveryService
    {
        // 'GetOrAdd' call on the dictionary is not thread safe and we might end up creating the GetterInfo more than
        // once. To prevent this Lazy<> is used. In the worst case multiple Lazy<> objects are created for multiple
        // threads but only one of the objects succeeds in creating a GetterInfo.
        private readonly ConcurrentDictionary<string, Lazy<ICollection<MvcControllerViewModel>>> _allSecuredActionsWithPloicy =
            new ConcurrentDictionary<string, Lazy<ICollection<MvcControllerViewModel>>>();

        /// <summary>
        /// MvcActions Discovery Service
        /// </summary>
        public MvcActionsDiscoveryService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            if (actionDescriptorCollectionProvider == null)
            {
                throw new ArgumentNullException(nameof(actionDescriptorCollectionProvider));
            }

            MvcControllers = new List<MvcControllerViewModel>();

            var lastControllerName = string.Empty;
            MvcControllerViewModel? currentController = null;

            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (var actionDescriptor in actionDescriptors)
            {
                if (!(actionDescriptor is ControllerActionDescriptor descriptor))
                {
                    continue;
                }

                var controllerTypeInfo = descriptor.ControllerTypeInfo;
                var actionMethodInfo = descriptor.MethodInfo;

                if (lastControllerName != descriptor.ControllerName)
                {
                    currentController = new MvcControllerViewModel
                    {
                        AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue!,
                        ControllerAttributes = getAttributes(controllerTypeInfo),
                        ControllerDisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName!,
                        ControllerName = descriptor.ControllerName,
                    };
                    MvcControllers.Add(currentController);

                    lastControllerName = descriptor.ControllerName;
                }

                currentController?.MvcActions.Add(new MvcActionViewModel
                {
                    ControllerId = currentController.ControllerId,
                    ActionName = descriptor.ActionName,
                    ActionDisplayName = actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName!,
                    ActionAttributes = getAttributes(actionMethodInfo),
                    IsSecuredAction = isSecuredAction(controllerTypeInfo, actionMethodInfo)
                });
            }
        }

        /// <summary>
        /// Returns the list of all of the controllers and action methods of an MVC application.
        /// </summary>
        public ICollection<MvcControllerViewModel> MvcControllers { get; }


        /// <summary>
        /// Returns the list of all of the controllers and action methods of an MVC application which have AuthorizeAttribute and the specified policyName.
        /// </summary>
        public ICollection<MvcControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName)
        {
            var getter = _allSecuredActionsWithPloicy.GetOrAdd(policyName, y => new Lazy<ICollection<MvcControllerViewModel>>(
                () =>
                {
                    var controllers = new List<MvcControllerViewModel>(MvcControllers);
                    foreach (var controller in controllers)
                    {
                        controller.MvcActions = controller.MvcActions.Where(
                            model => model.IsSecuredAction &&
                            (
                            model.ActionAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName ||
                            controller.ControllerAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName
                            )).ToList();
                    }
                    return controllers.Where(model => model.MvcActions.Any()).ToList();
                }));
            return getter.Value;
        }

        private static List<Attribute> getAttributes(MemberInfo actionMethodInfo)
        {
            return actionMethodInfo.GetCustomAttributes(inherit: true)
                                   .Where(attribute =>
                                   {
                                       var attributeNamespace = attribute.GetType().Namespace;
                                       return attributeNamespace != typeof(CompilerGeneratedAttribute).Namespace &&
                                              attributeNamespace != typeof(DebuggerStepThroughAttribute).Namespace;
                                   })
                                    .Cast<Attribute>()
                                   .ToList();
        }

        private static bool isSecuredAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            var actionHasAllowAnonymousAttribute = actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(inherit: true) != null;
            if (actionHasAllowAnonymousAttribute)
            {
                return false;
            }

            var controllerHasAuthorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
            if (controllerHasAuthorizeAttribute)
            {
                return true;
            }

            var actionMethodHasAuthorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
            if (actionMethodHasAuthorizeAttribute)
            {
                return true;
            }

            return false;
        }

    }
}