import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";
import { delayedTimeout } from "../utilities/delayedTimeout";

export const getEmployeeTypes = createAsyncThunk(
    "employee/getEmployeeTypes",
    async(ThunkApi, {rejectedWhitValue} ) =>{
        try{
            const {data} = await axios.get('api/v1/Empleado/getEmployeeTypes');
            return data;
        }catch(err){
            return rejectedWhitValue(err.response.data.message);
        }
    }
);

export const registerEmployee = createAsyncThunk(
    "employee/registerEmployee",
    async (params, { rejectWithValue }) => {
      try {
        const requestConfig = {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        };
  
        const { data } = await axios.post(
          `/api/v1/Empleado/registerEmployee`,
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
