var handleRenderHighlight=function(){$('.hljs-wrapper pre code').each(function(i,block){hljs.highlightElement(block);});};var Highlight=function(){"use strict";return{init:function(){handleRenderHighlight();}};}();$(document).ready(function(){Highlight.init();});