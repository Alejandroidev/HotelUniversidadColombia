using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {
        // Usando tus credenciales de Cloudinary
        var account = new Account(
            "hotel",   // Reemplaza con tu Cloud Name
            "827314335838485",      // Reemplaza con tu API Key
            "fAJvOugqBjQSJVhmIPq5Wbl7md4"    // Reemplaza con tu API Secret
        );

        _cloudinary = new Cloudinary(account);
    }

    public string UploadImage(string filePath)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(filePath)  // Ruta de tu imagen
        };

        var uploadResult = _cloudinary.Upload(uploadParams);

        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return uploadResult.SecureUrl.AbsoluteUri;  // URL segura de la imagen subida
        }

        return null;  // En caso de error
    }
}