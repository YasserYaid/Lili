import { createSlice } from "@reduxjs/toolkit";
import {getEmployeeTypes} from "../actions/employeeAction";

export const initialState = {
    employeeTypes : [],
    loading : false,
    error : null
};

export const employeeTypesSlice = createSlice({
    name : "employeeTypes",
    initialState,
    reducers : {},
    extraReducers : {
        [getEmployeeTypes.pending] : (state) =>{
            state.loading = true;
            state.error = null;
        },
        [getEmployeeTypes.fulfilled] : (state, {payload}) => {
            state.loading = false;
            state.error = null;
            state.employeeTypes = payload;
        },
        [getEmployeeTypes.rejected] : (state, action) =>{
            state.loading = false;
            state.error = action.payload;
        }
    }
});

export const employeeTypesReducer = employeeTypesSlice.reducer;