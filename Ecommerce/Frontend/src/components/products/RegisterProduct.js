import React, { useEffect, useState, Fragment } from "react";
import MetaData from "../layout/MetaData";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { registerProduct } from "../../actions/productsAction";

const RegisterProduct = () => {
  const navigate = useNavigate;
  const alert = useAlert();
  const dispatch = useDispatch();

  const [product, setProduct] = useState({
    nombre: "",
    precio: "",
    descripcion: "",
    cantidadDisponible:"",
    fechaCaducidad:"",
    categoria:"",
    sucursal:"",
    categoryId:"",
    branchId:"",
    vendedor:"",
    foto: ""
  });

  const { nombre, precio, descripcion, cantidadDisponible, fechaCaducidad} = product;

  const [productImage, setproductImage] = useState("");

  const [productImagePreview, setproductImagePreview] = useState(
    "images/default_product.png"
  );

  const { categories } = useSelector((state) => state.category);
  let [categoria, setCategoria] = useState(product ? product.categoria : "Pasteles");
  let [categoryId, setCategoryId] = useState(product ? product.categoryId : 1);
  const { branches } = useSelector((state) => state.sucursalesRedu);
  let [sucursal, setSucursal] = useState(product ? product.vendedor : "Polita");
  let [branchId, setBranchId] = useState(product ? product.branchId : 1);

  const { error, isRegistered, loading } = useSelector((state) => state.products);

  useEffect(() => {
    if (categories.length > 0) {
      categoria = categories[0].nombre;
      categoryId = categories[0].id;
    }
    if(branches.length > 0 ){
      sucursal = branches[0].nombreComercial;
      branchId = branches[0].id;
    }

    if (isRegistered) {
      alert.info("Producto registrado con exito");
    }

    if (error) {
      alert.error(error);
    }
  }, [dispatch, alert, isRegistered, error, navigate, categories, branches]);

  const submitHandler = (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.set("nombre", nombre);
    formData.set("precio", precio);
    formData.set("descripcion", descripcion);
    formData.set("stock", cantidadDisponible);
    formData.set("cantidadDisponible", cantidadDisponible);
    formData.set("fechaCaducidad", fechaCaducidad);
    formData.set("categoria", categoria);
    formData.set("sucursal", sucursal);
    formData.set("categoryId", categoryId);
    formData.set("branchId", branchId);
    formData.set("vendedor", sucursal);
    formData.set("foto", productImage);

    dispatch(registerProduct(formData));
  };

  const onChange = (e) => {
    if (e.target.name === "productImage") {
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.readyState === 2) {
          setproductImagePreview(reader.result);
          setproductImage(e.target.files[0]);
        }
      };

      reader.readAsDataURL(e.target.files[0]);
    } else {
      setProduct({ ...product, [e.target.name]: e.target.value });
    }
  };

  return (
    <Fragment>
      <MetaData title={"Registro De Producto"} />
      <div className="row wrapper">
        <div className="col-10 col-lg-5">
          <form
            className="shadow-lg"
            encType="multipart/form-data"
            onSubmit={submitHandler}
          >
            <h1 className="mb-3">Registrar Producto</h1>

            <div className="form-group">
              <label htmlFor="nombre_field">Nombre</label>
              <input
                type="text"
                id="nombre_field"
                className="form-control"
                value={nombre}
                name="nombre"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="precio_field">Precio</label>
              <input
                type="number"
                id="precio_field"
                className="form-control"
                value={precio}
                name="precio"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="descripcion_field">Descripcion</label>
              <input
                type="text"
                id="descripcion_field"
                className="form-control"
                value={descripcion}
                name="descripcion"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="cantidadDisponible_field">Cantidad Disponible</label>
              <input
                type="number"
                id="cantidadDisponible_field"
                className="form-control"
                value={cantidadDisponible}
                name="cantidadDisponible"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="fechaCaducidad_field">Fecha Caducidad</label>
              <input
                type="date"
                id="fechaCaducidad_field"
                className="form-control"
                value={fechaCaducidad}
                name="fechaCaducidad"
                onChange={onChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="categoria_field">Categoria</label>
              <select
                id="categoria_field"
                className="form-control"
                value={categoria ?? "Pastel"}
                name="categoria"
                onChange={(e) => {
                  setCategoria(e.target.value);
                  categoria = e.target.value;
                  setCategoryId(e.target.key);
                  categoryId = e.target.key;
                }}
                required
              >
                {categories.map((category) => (
                  <option key={category.id} value={category.nombre}>
                    {category.nombre}
                  </option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label htmlFor="sucursal_field">Sucursal</label>
              <select
                id="sucursal_field"
                className="form-control"
                value={sucursal ?? "POLITA"}
                name="sucursal"
                onChange={(e) => {
                  setSucursal(e.target.value);
                  sucursal = e.target.value;
                  setBranchId(e.target.key);
                  branchId = e.target.key;
                }}
                required
              >
                {branches.map((branch) => (
                  <option key={branch.id} value={branch.nombreComercial}>
                    {branch.nombreComercial}
                  </option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label htmlFor="productImage_upload">Imagen de producto</label>
              <div className="d-flex align-items-center">
                <div>
                  <figure className="productImage mr-3 item-rtl">
                    <img
                      src={productImagePreview}
                      className="rounded-circle"
                      alt="Imagen Previa"
                    />
                  </figure>
                </div>
                <div className="custom-file">
                  <input
                    type="file"
                    name="productImage"
                    className="custom-file-input"
                    id="customFile"
                    accept="images/*"
                    onChange={onChange}
                  />
                  <label className="custom-file-label" htmlFor="customFile">
                    Subir imagen de producto
                  </label>
                </div>
              </div>
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

export default RegisterProduct;
