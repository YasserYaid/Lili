import { createSlice } from "@reduxjs/toolkit";
import { getCountryStates } from "../actions/countryStatesAction";

export const initialState = {
  countryStates: [],
  loading: false,
  error: null,
};

export const countryStateSlice = createSlice({
  name: "statesOfCountrys",
  initialState,
  reducers: {},
  extraReducers: {
    [getCountryStates.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },

    [getCountryStates.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.countryStates = payload;
    },

    [getCountryStates.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const countryStateReducer = countryStateSlice.reducer;
