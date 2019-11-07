import React, { Component } from 'react'

/*
class ProductHeaderRow extends Component {
    render() {
        return (
            <tr>
                <th>Product Id</th>
                <th>Manufacturing Plan Name</th>
            </tr>
        )
    }
}

class ProductRow extends Component {
    render() {
        const productid = this.props.productid;
        const name = this.props.name;
        return (
            <tr>
                <td>{productid}</td>
                <td>{name}</td>
            </tr>
        )
    }
}

class ProductTable extends React.Component {
    render() {
        const rows = [];
        rows.push(
            <ProductHeaderRow/>
        )
        this.props.products.forEach((product) => {
            rows.push(
                <ProductRow
                    product={product}
                    key={product.name} />
            );
        });

        return (
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>{rows}</tbody>
            </table>
        );
    }
}

*/
const Products = ({ products }) => {
    return (
        <table className="table">
            <thead>
                <tr>
                    <th scope="col">Product Id</th>
                    <th scope="col">Manufacturing Plan Name</th>
                </tr>
            </thead>
            <tbody>
                {products.map((product) => (
                    <tr>
                        <td>{product.id} </td>
                        <td>{product.plan.name} </td>
                    </tr>
                ))}
            </tbody>
        </table>
    )
};

export default Products;