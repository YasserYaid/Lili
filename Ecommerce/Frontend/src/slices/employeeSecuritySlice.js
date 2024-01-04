import {createSlice} from '@reduxjs/toolkit';
import {registerEmployee} from '../actions/employeeAction';

const initialState = {
    loading: false,
    errores: [],
    employee: null,
    isRegistered : false
};

export const employeeSecuritySlice = createSlice({
    name: "employeeSecurity",
    initialState,
    reducers: {
        resetError: (state) => {
            state.loading = false;
            state.errores = [];
            state.employee = null;
            state.isRegistered = false;
        }
    },
    extraReducers: {
        [registerEmployee.pending] : (state) => {
            state.loading = true;
            state.errores = [];
            state.employee = null;
            state.isRegistered = false;
        },
        [registerEmployee.fulfilled]: (state, {payload}) => {
            state.loading= false;
            state.employee= payload;
            state.errores= [];
            state.isRegistered = true;
            console.log("RegisteredProduct payload", payload);
        },
        [registerEmployee.rejected]: (state, action) => {
            state.loading = false;
            state.errores = action.payload;
            state.employee = null;
            state.isRegistered = false;
        },
    }
});

export const {resetError} = employeeSecuritySlice.actions;
export const employeeSecurityReducer = employeeSecuritySlice.reducer;