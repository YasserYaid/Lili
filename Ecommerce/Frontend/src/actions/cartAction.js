import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from '../utilities/axios';

export const getShoppingCart = createAsyncThunk(
    "shoppingCart/GetById",
    async({rejectWithValue}) => {

        try{
            
 const shoppingCartId = localStorage.getItem("shoppingCartId") ?? '00000000-0000-0000-0000-000000000000';
 // si ya habia un carrito anteriormente en local storage lo obtiene
 //sino se le asigna un generico para que se cree en la api despues
        const {data}  = await axios.get(`/api/v1/ShoppingCart/${shoppingCartId}`);

            return data;
        }catch(err)
        {
            return rejectWithValue(err.response.data.message);

        }

    }
);


export const addItemShoppingCart = createAsyncThunk(
    "shoppingCart/update",
    async(params, {rejectWithValue}) => {

        try{
            const  { shoppingCartItems, item, cantidad} = params;

            let items = [];

            items = shoppingCartItems.slice();// Se crea un clon de lo almacenado en el slice

            const indice = shoppingCartItems.findIndex(i => i.productId === item.productId); // se busca el elemento a agregar
            //en el vector mediante el indice y el product id

            if(indice === -1)
            {
                items.push(item);// si es un carrito vacio solo se agrega
            }else{// sino lo incremeta
                let cantidad_ = items[indice].cantidad;
                var total = cantidad_ + cantidad;
                let itemNewClone = {...items[indice]};
                itemNewClone.cantidad = total;
                items[indice] = itemNewClone;
            }

            var request = {
                shoppingCartItems: items///se envian los items almacenados
            }

            const requestConfig = {
                headers: {
                    "Content-Type": "application/json",
                }
            }

            const {data} = await axios.put(
                `/api/v1/ShoppingCart/${params.shoppingCartId}`,// se almacena los items en el carrito con el id
                request,
                requestConfig
            )

            return data;
        }
        catch(err)
        {
            rejectWithValue(err.response.data.message);
        }


    }
)


export const removeItemShoppingCart = createAsyncThunk(
    "shoppingCart/removeItem",
    async(params, { rejectWithValue }) => {

        try{
            const requestConfig = {
                headers: {
                    "Content-Type": "application/json"
                }
            }

            const {data} = await axios.delete(
                `/api/v1/ShoppingCart/item/${params.id}`,// se elimina el item del shoping car que tenga el id
                params,
                requestConfig
            );

            return data;

        }catch(err){

            return rejectWithValue(err.response.data.message);
        }



    }
)






export const saveAddressInfo = createAsyncThunk(
    "shoppingCart/saveAddressInfo",
    async(params, {rejectWithValue}) => {

        try {
            const requestConfig = {
                headers : {
                    "Content-Type": "application/json",
                }
            };

            const {data} = await axios.post(
                `api/v1/order/address`,
                params,
                requestConfig
            );

            return data;
        }catch(err)
        {

            return rejectWithValue(err.response.data.message);
        }

    }
)