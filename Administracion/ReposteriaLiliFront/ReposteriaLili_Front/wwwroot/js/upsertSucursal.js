//Variables
let mapa = null; //Mapa de google maps}
//Ubicacion de la FEI
let latitud = 19.541142;
let longitud = -96.9271873;
//Ubicacion del cliente
let latitudHome;
let longitudHome;
//let transportesSelect = document.getElementById("Transporte");
//let rutaCheck = document.querySelector("#Ruta");
let directionsRenderer = new google.maps.DirectionsRenderer();
// Esta funcion dibuja el mapa y coloca un marcador seleccionable en la FEI

function dibujaMapa() {
    mapa = $('#mapa').locationpicker({
        location: { latitude: latitud, longitude: longitud },
        radius: 300,
        addressFormat: 'point_of_interest',
        inputBinding: {
            latitudeInput: $('#Latitud'),
            longitudeInput: $('#Longitud'),
        },
        enableAutoComplete: true,
        enableReverseGeocode: true,
        onchanged: function (currentLocation, radius, isMarkerDropped) {
            latitud = currentLocation.latitude;
            longitud = currentLocation.longitude;
            //distancia();
        }
    });
}


// Se inicia cuando la pagina ha cargado por compelto
$(function () {
    dibujaMapa();
    //miUbicacion();
});