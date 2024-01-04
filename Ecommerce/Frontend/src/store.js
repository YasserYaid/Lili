import {configureStore } from "@reduxjs/toolkit";
import { productsReducer } from "./slices/productsSlice";
import { productByIdReducer } from "./slices/productByIdSlice";
import { productPaginationReducer } from "./slices/productPaginationSlice";
import { categoryReducer } from "./slices/categorySlice";
import { securityReducer } from "./slices/securitySlice";
import { employeeSecurityReducer } from "./slices/employeeSecuritySlice";
import { forgotPasswordReducer } from "./slices/forgotPasswordSlice";
import { resetPasswordReducer } from "./slices/resetPasswordSlice";
import { cartReducer } from "./slices/cartSlice";
import { countryReducer } from "./slices/countrySlice";
import { orderReducer } from "./slices/orderSlice";
import {employeeTypesReducer} from "./slices/employeeTypesSlice";
import { branchReducer } from "./slices/branchSlice";
import { countryStateReducer } from "./slices/countryStateSlice";


export default configureStore({
    reducer: {
        products: productsReducer,
        product: productByIdReducer,
        productPagination: productPaginationReducer,
        category: categoryReducer,
        security: securityReducer,
        employeeSecurity : employeeSecurityReducer,
        forgotPassword: forgotPasswordReducer,
        resetPassword: resetPasswordReducer,
        cart: cartReducer,
        country: countryReducer,
        order: orderReducer,
        employeeTypes : employeeTypesReducer,
        sucursalesRedu : branchReducer,
        statesByCountry : countryStateReducer,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({serializableCheck: false})
})