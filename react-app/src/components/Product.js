import React, {useEffect, useState} from 'react'
import Loading from './Loading'
import {FaPlus, FaPencilAlt} from 'react-icons/fa';
import config from '../Config'
import axios from "axios";


function Products() {
    const [products, setProducts] = useState([]);
    //adicionar const para Manuf. Plans
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            const allProducts = await axios(config.routes.products.getAll);
            //const allMachineTypes = await axios(config.routes.machinetypes.getAll);
            setProducts(allProducts.data);
            //setMachineTypes(allMachineTypes.data);
            setIsLoading(false);
        };

        fetchData();
    }, []);

    return (
        <>
            <h3>Products</h3>
            {
                isLoading ? <Loading/> :
                    (
                        <>
                            <ProductTable products={products} /*{machineTypes={machineTypes}}*/ />
                            {/* <CreateMachine machineTypes={machineTypes} />*/}
                            <CreateProduct/>
                        </>
                    )
            }
        </>
    )
}

function ProductTable(props) {
    return (
        <table className="table">
            <thead>
            <tr>
                <th scope="col">Id</th>
                <th scope="col">Manufacturing Plan</th>
                <th></th>
            </tr>
            </thead>
            <tbody>
            {props.products.map(product => (
                <ProductTableRow key={product.id} product={product} /*machineTypes={props.machineTypes}*//>
            ))}
            </tbody>
        </table>
    )
}

function ProductTableRow(props) {
    /* const product = props.products.find(function (product) {
         if (product.id === props.product.id) {
             return product;
         }
     });*/

    return (
        <tr>
            <td>{props.product.id}</td>
            <td>{props.product.plan.name}</td>
            <td>
                <button type="button" className="btn btn-outline-primary btn-sm ">
                    <i><FaPencilAlt/></i>
                </button>
            </td>
        </tr>
    )
}

function CreateProduct(props) {
    const [manufacturingPlan, setManufacturingPlan] = useState("");

    const handleSubmit = event => {
        event.preventDefault();

        const requestHeader = {
            "Content-Type": "application/json;charset=UTF-8",
        }
        const requestBody = {
            manufacturingplan: manufacturingPlan
        }

        axios.post(config.routes.products.createProduct, requestBody, requestHeader);
        axios({
            method: "post",
            url: config.routes.products.createProduct,
            data: requestBody,
            headers: requestHeader,
        })

    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-row">
                <div className="form-group col-md-6">
                    <label htmlFor="manufacturingPlan">Manufacturing Plan</label>
                    <input type="text" className="form-control" id="manufacturingPlan"
                           onChange={e => setManufacturingPlan(e.target.value)}/>
                </div>
            </div>
            <button type="submit" className="btn btn-primary">Create</button>
        </form>
    )
}

export default Products;


