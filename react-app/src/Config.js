import React from "react";

const ENDPOINTS_DEV = {
    masterdatafactory: "https://localhost:5001/",
    masterdataproduct: "https://localhost:5001/"
}

const ENDPOINTS_PROD = {
    masterdatafactory: "https://masterdatafactory.azurewebsites.net/",
    masterdataproduct: "https://masterdataproduct.azurewebsites.net/"
}


/*
const internalroutes = {
  "/machines": () => <Machine />,
  "/machinetypes": () => <MachineType />
};
*/
const ENDPOINTS = process.env.NODE_ENV === 'development' ? ENDPOINTS_DEV : ENDPOINTS_PROD

export default {
    endpoints: ENDPOINTS,
    routes: {
        machines: {
            getAll: `${ENDPOINTS.masterdatafactory}api/machine`,
            createMachine: `${ENDPOINTS.masterdatafactory}api/machine`,
            filterByMachineType: `${ENDPOINTS.masterdatafactory}api/machine/machinetype/`,
        },
        machinetypes: {
            getAll: `${ENDPOINTS.masterdatafactory}api/machinetype`,
            createMachineType: `${ENDPOINTS.masterdatafactory}api/machinetype`,
        },
        products: {
            getAll: `${ENDPOINTS.masterdataproduct}api/product`,
            createProduct: `${ENDPOINTS.masterdataproduct}api/product`
        },    
        operations:{
            getById: `${ENDPOINTS.masterdatafactory}api/operation/`,
            createOperation: `${ENDPOINTS.masterdatafactory}api/operation`
        }
    }
}

