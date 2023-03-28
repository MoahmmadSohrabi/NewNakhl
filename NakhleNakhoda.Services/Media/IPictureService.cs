using Microsoft.AspNetCore.Http;
using NakhleNakhoda.Data.Core;
using NakhleNakhoda.Domain.Media;

namespace NakhleNakhoda.Services.Media
{
    /// <summary>
    /// Picture service interface
    /// </summary>
    public partial interface IPictureService : IRepository<Picture>
    {
        /// <summary>
        /// Gets the picture binary array
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Download binary array</returns>
        byte[] GetPictureBits(IFormFile file);

        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        string? GetFileExtensionFromMimeType(string mimeType);

        /// <summary>
        /// Gets the loaded picture binary depending on picture storage settings
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <returns>Picture binary</returns>
        Task<byte[]> LoadPictureBinary(Picture picture);

        /// <summary>
        /// Get picture SEO friendly name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        string GetPictureSeName(string name);

        /// <summary>
        /// Gets the default picture URL
        /// </summary>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Picture URL</returns>
        Task<string> GetDefaultPictureUrl(int targetSize = 0,
            PictureType defaultPictureType = PictureType.Entity,
            string? storeLocation = null);

        /// <summary>
        /// Get a picture URL
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <returns>Picture URL</returns>
        Task<string> GetPictureUrl(long pictureId,
            int targetSize = 0,
            bool showDefaultPicture = true,
            string? storeLocation = null,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// Get a picture URL
        /// </summary>
        /// <param name="picture">Reference instance of Picture</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <returns>Picture URL</returns>
        Task<string> GetPictureUrl(Picture picture,
            int targetSize = 0,
            bool showDefaultPicture = true,
            string? storeLocation = null,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// Get a picture local path
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <returns></returns>
        Task<string> GetThumbLocalPath(Picture picture, int targetSize = 0, bool showDefaultPicture = true);

        /// <summary>
        /// Gets a picture
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <returns>Picture</returns>
        Task<Picture?> GetPictureById(long pictureId);

        /// <summary>
        /// Deletes a picture
        /// </summary>
        /// <param name="picture">Picture</param>
        Task DeletePicture(Picture picture);

        /// <summary>
        /// Gets a collection of pictures
        /// </summary>
        /// <param name="virtualPath">Virtual path</param>
        /// <param name="pageIndex">Current page</param>
        /// <param name="pageSize">Items on each page</param>
        /// <returns>Paged list of pictures</returns>
        IPagedList<Picture?> GetPictures(string virtualPath = "", int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Inserts a picture
        /// </summary>
        /// <param name="pictureBinary">The picture binary</param>
        /// <param name="mimeType">The picture MIME type</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <param name="altAttribute">"alt" attribute for "img" HTML element</param>
        /// <param name="titleAttribute">"title" attribute for "img" HTML element</param>
        /// <param name="isNew">A value indicating whether the picture is new</param>
        /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
        /// <returns>Picture</returns>
        Task<Picture?> InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
            string? altAttribute = null, string? titleAttribute = null,
            bool isNew = true, bool validateBinary = true);

        /// <summary>
        /// Inserts a picture
        /// </summary>
        /// <param name="formFile">Form file</param>
        /// <param name="defaultFileName">File name which will be use if IFormFile.FileName not present</param>
        /// <param name="virtualPath">Virtual path</param>
        /// <returns>Picture</returns>
        Task<Picture?> InsertPicture(IFormFile formFile, string defaultFileName = "", string virtualPath = "");

        /// <summary>
        /// Updates the picture
        /// </summary>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="pictureBinary">The picture binary</param>
        /// <param name="mimeType">The picture MIME type</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <param name="altAttribute">"alt" attribute for "img" HTML element</param>
        /// <param name="titleAttribute">"title" attribute for "img" HTML element</param>
        /// <param name="isNew">A value indicating whether the picture is new</param>
        /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
        /// <returns>Picture</returns>
        Task<Picture?> UpdatePicture(long pictureId, byte[] pictureBinary, string mimeType,
            string seoFilename, string? altAttribute = null, string? titleAttribute = null,
            bool isNew = true, bool validateBinary = true);

        /// <summary>
        /// Updates the picture
        /// </summary>
        /// <param name="picture">The picture to update</param>
        /// <returns>Picture</returns>
        Task<Picture?> UpdatePicture(Picture picture);

        /// <summary>
        /// Updates a SEO filename of a picture
        /// </summary>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="seoFilename">The SEO filename</param>
        /// <returns>Picture</returns>
        Task<Picture?> SetSeoFilename(long pictureId, string seoFilename, string? altAttribute = null, string? titleAttribute = null);

        /// <summary>
        /// Validates input picture dimensions
        /// </summary>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary or throws an exception</returns>
        byte[] ValidatePicture(byte[] pictureBinary, string mimeType);

        /// <summary>
        /// Gets or sets a value indicating whether the images should be stored in data base.
        /// </summary>
        bool StoreInDb { get; }

        /// <summary>
        /// Get pictures hashes
        /// </summary>
        /// <param name="picturesIds">Pictures Ids</param>
        /// <returns></returns>
        //IDictionary<int, string> GetPicturesHash(int[] picturesIds);

        /// <summary>
        /// Get picture binary by picture identifier
        /// </summary>
        /// <param name="pictureId">The picture identifier</param>
        /// <returns>Picture binary</returns>
        Task<PictureBinary?> GetPictureBinaryByPictureId(long pictureId);
    }
}