using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.ImageManagement;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.ImageFirebase
{
    public class ManageImageFirebaseService
    {
        public FirebaseSettings _firebaseSettings { get; }

        public ManageImageFirebaseService() { 
            _firebaseSettings = new FirebaseSettings("tiangusitrek@gmail.com","*F1rebaseC#app!*","reposterialili-8b9ea.appspot.com","Profile_Images","Product_Images", "Product_BarCodes", "AIzaSyCY-W6_ic1FX5-JsCtKxgMCW42N3IpBU94");
        }

        public async Task<string> UploadImage(Stream image, string name, string typeImage)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSettings.Api_key));
            var authentication = await authProvider.SignInWithEmailAndPasswordAsync(_firebaseSettings.Email, _firebaseSettings.Password);

            var cancellation = new CancellationTokenSource();

            string pathToSave = "";
            if (typeImage == "PROFILE") pathToSave = _firebaseSettings.RutaImagenPerfil;
            if (typeImage == "PRODUCT") pathToSave = _firebaseSettings.RutaImagenProducto;
            if (typeImage == "BARCODE") pathToSave = _firebaseSettings.RutaImagenCodigoBarra;


            var task = new FirebaseStorage(_firebaseSettings.RutaRaiz, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authentication.FirebaseToken),
                ThrowOnCancel = true
            }).Child(pathToSave).Child(name).PutAsync(image, cancellation.Token);

            var downloadUrl = await task;
            return downloadUrl;
        }
    }
}
