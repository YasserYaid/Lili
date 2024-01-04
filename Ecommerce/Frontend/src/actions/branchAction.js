import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";
import { delayedTimeout } from "../utilities/delayedTimeout";

export const getBranches = createAsyncThunk(
    "branch/getBranches",
    async(ThunkApi, {rejectWithValue} ) =>{
        try{
            const {data} = await axios.get('/api/Branch/getBranches');
            return data;
        }catch(err){
            return rejectWithValue(err.response.data.message);
        }
    }
);

export const registerBranch = createAsyncThunk(
    "branch/registerBranch",
    async (params, { rejectWithValue }) => {
      try {
        const requestConfig = {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        };
  
        const { data } = await axios.post(
          `/api/Branch/registerBranch`,
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