import { createSlice } from "@reduxjs/toolkit";
import { getBranches , registerBranch } from "../actions/branchAction";

export const initialState = {
  branches: [],
  loading: false,
  error: null,
  branch: null,
  isRegistered: false
};

export const branchSlice = createSlice({
  name: "branches",
  initialState,
  reducers: {},
  extraReducers: {
    [getBranches.pending]: (state) => {
      state.loading = true;
      state.error = null;
      state.branches = [];
    },

    [getBranches.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.branches = payload;
    },

    [getBranches.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
      state.branches = [];
    },
    [registerBranch.pending]: (state) => {
      state.loading = true;
      state.error = null;
      state.branch = null;
      state.isRegistered = false;
    },
    [registerBranch.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.error = null;
      state.branch = payload;
      state.isRegistered = true;
      console.log("RegisteredProduct payload", payload);
    },
    [registerBranch.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
      state.branch = null;
      state.isRegistered = false;
    },
  },
});

export const branchReducer = branchSlice.reducer;
