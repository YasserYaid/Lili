import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from  '../utilities/axios';
import { delayedTimeout } from "../utilities/delayedTimeout";
import { httpParams } from "../utilities/httpParams";

export const registerProduct = createAsyncThunk(
    "product/registerProduct",
    async (params, { rejectWithValue }) => {
      try {
        const requestConfig = {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        };
  
        const { data } = await axios.post(
          `/api/v1/Product/register`,
          params,
          requestConfig
        );  
        await delayedTimeout(1000);
        
        return data;
      } catch (err) {
        return rejectWithValue(err.response.data.message);
      }
    }
  );

export const getProducts = createAsyncThunk(
    "products/getProducts",
    async (ThunkApi, {rejectWithValue}) => {
        
        try{
            await delayedTimeout(1000);
            return await axios.get(`/api/v1/product/list`);
        }catch(err){
            return rejectWithValue(`Errores: ${err.message}`);
        }

    }
)

export const getProductById = createAsyncThunk(
    "products/getProductId",
    async (id, {rejectWithValue}) => {
        try {
            await delayedTimeout(1000);
            return await axios.get(`/api/v1/product/${id}`);
        }catch(err)
        {
            return rejectWithValue(`Errores: ${err.message}`);
        }
    }
)

export const getProductPagination = createAsyncThunk(
    "products/getProductPagination",
    async(params, { rejectWithValue }) => {

        try{
            await delayedTimeout(1000); 
            params = httpParams(params);
            const paramUrl = new URLSearchParams(params).toString();

            var results = axios.get(`api/v1/product/pagination?${paramUrl}`);

            return (await results).data;

        }catch(err)
        {
            return rejectWithValue(`Errores: ${err.message}`);
        }
    }
);
