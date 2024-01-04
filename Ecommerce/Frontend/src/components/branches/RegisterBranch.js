import React, { useEffect, useState, Fragment } from "react";
import MetaData from "../layout/MetaData";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { registerBranch } from "../../actions/branchAction";

const RegisterBranch = () => {
  const navigate = useNavigate;
  const alert = useAlert();
  const dispatch = useDispatch();

  const [branch, setBranch] = useState({
    calle:"",
    ciudad:"",
    codigoPostal:"",
    colonia:"",
    diaInicial:"",
    diaFinal:"",
    estado:"",
    horaInicial:"",
    horaFinal:"",
    latitud:"",
    longitud:"",
    municipio:"",
    nombreComercial:"",
    numeroDepartamento:"",
    foto:""
  });

  const { calle, ciudad, codigoPostal, colonia, diaInicial, diaFinal, horaInicial, horaFinal, 
          latitud, longitud, municipio, nombreComercial, numeroDepartamento } = branch;

  const { countryStates } = useSelector((state) => state.statesByCountry);
  let [estado, setEstado] = useState(branch ? branch.estado : "Veracruz");

  const { error, isRegistered, loading } = useSelector((state) => state.sucursalesRedu);

  useEffect(() => {
    if (countryStates.length > 0) {
      estado = countryStates[0].name;
    }

    if (isRegistered) {
      alert.info("Sucursal registrada con exito");
    }

    if (error) {
      alert.error(error);
    }
  }, [dispatch, alert, isRegistered, error, navigate, countryStates]);

  const submitHandler = (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.set("calle", calle);
    formData.set("ciudad", ciudad);
    formData.set("codigoPostal", codigoPostal);
    formData.set("colonia", colonia);
    formData.set("diaInicial", diaInicial);
    formData.set("diaFinal", diaFinal);
    formData.set("estado", estado);
    formData.set("horaInicial", horaInicial);
    formData.set("horaFinal", horaFinal);
    formData.set("latitud", latitud);
    formData.set("longitud", longitud);
    formData.set("municipio", municipio);
    formData.set("nombreComercial", nombreComercial);
    formData.set("numeroDepartamento", numeroDepartamento);
    dispatch(registerBranch(formData));
  };

  const onChange = (e) => { 
      setBranch({ ...branch, [e.target.name]: e.target.value });
  };

  return (
    <Fragment>
      <MetaData title={"Registro De Sucursal"} />
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form
            className="shadow-lg"
            encType="multipart/form-data"
            onSubmit={submitHandler}
          >
            <h1 className="mb-3">Registrar Sucursal</h1>

            <div className="form-group">
              <label htmlFor="nombreComercial_field">Nombre Comercial</label>
              <input
                type="text"
                id="nombreComercial_field"
                className="form-control"
                value={nombreComercial}
                name="nombreComercial"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="stateCountry_field">Estado</label>
              <select
                id="stateCountry_field"
                className="form-control"
                value={estado ?? "Veracruz"}
                name="estado"
                onChange={(e) => {
                  setEstado(e.target.value);
                  estado = e.target.value;
                }}
                required
              >
                {countryStates.map((countryState) => (
                  <option key={countryState.id} value={countryState.name}>
                    {countryState.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label htmlFor="ciudad_field">Ciudad</label>
              <input
                type="text"
                id="ciudad_field"
                className="form-control"
                value={ciudad}
                name="ciudad"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="municipio_field">Municipio</label>
              <input
                type="text"
                id="municipio_field"
                className="form-control"
                value={municipio}
                name="municipio"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="calle_field">Calle</label>
              <input
                type="text"
                id="calle_field"
                className="form-control"
                value={calle}
                name="calle"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="numeroDepartamento_field">Numero</label>
              <input
                type="number"
                id="numeroDepartamento_field"
                className="form-control"
                value={numeroDepartamento}
                name="numeroDepartamento"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="colonia_field">Colonia</label>
              <input
                type="text"
                id="colonia_field"
                className="form-control"
                value={colonia}
                name="colonia"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="codigoPostal_field">Codigo Postal</label>
              <input
                type="text"
                id="codigoPostal_field"
                className="form-control"
                value={codigoPostal}
                name="codigoPostal"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="diaInicial_field">Dia Laboral Inicial</label>
              <input
                type="text"
                id="diaInicial_field"
                className="form-control"
                value={diaInicial}
                name="diaInicial"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="diaFinal_field">Dia Laboral Final</label>
              <input
                type="text"
                id="diaFinal_field"
                className="form-control"
                value={diaFinal}
                name="diaFinal"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="horaInicial_field">Hora laboral inicial</label>
              <input
                type="time"
                id="horaInicial_field"
                className="form-control"
                value={horaInicial}
                name="horaInicial"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="horaFinal_field">Hora laboral final</label>
              <input
                type="time"
                id="horaFinal_field"
                className="form-control"
                value={horaFinal}
                name="horaFinal"
                onChange={onChange}
              />
            </div>

            <button
              id="register_button"
              type="submit"
              className="btn btn-block py-3"
            >
              Registrar
            </button>
          </form>
        </div>
      </div>
    </Fragment>
  );
};

export default RegisterBranch;
