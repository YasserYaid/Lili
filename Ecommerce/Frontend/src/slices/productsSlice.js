import { createSlice } from "@reduxjs/toolkit";
import { getProducts , registerProduct } from "../actions/productsAction";

export const initialState = {
  products: [],
  loading: false,
  error: null,
  product: null,
  isRegistered : false
};

export const productsSlice = createSlice({
  name: "products",
  initialState,
  reducers: {},
  extraReducers: {
    [getProducts.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getProducts.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.products = payload.data;
      state.error = null;
    },
    [getProducts.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
    [registerProduct.pending]: (state) => {
      state.loading = true;
      state.error = null;
      state.product = null;
      state.isRegistered = false;
    },
    [registerProduct.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.product = payload;
      state.isRegistered = true;
      console.log("RegisteredProduct payload", payload);
    },
    [registerProduct.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
      state.product = null;
      state.isRegistered = false;
    },
  },
});

export const productsReducer = productsSlice.reducer;
