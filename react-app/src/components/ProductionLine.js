import React, { useState, useEffect } from 'react'
import axios from "axios";
import Loading from './Loading'
import ProductionLineTable from './tables/ProductionLine/ProductionLineTable'
import ProductionLineNavBar from './bars/ProductionLine/ProductionLineNavBar'
import CreateProductionLineForm from './forms/ProductionLine/CreateProductionLineForm'
import UpdateProductionLineForm from './forms/ProductionLine/UpdateProductionLineForm'
import config from '../Config'
import Modal from 'react-bootstrap/Modal';


const ProductionLine = () => {

    const [productionLines, setProductionLines] = useState([])
    const [productionLineTypes, setProductionLineTypes] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    const [showCreate, setShowCreate] = useState(false)

    const handleCreateShow = () => setShowCreate(true)
    const handleCreateHide = () => setShowCreate(false)

    const [showUpdate, setShowUpdate] = useState(false)

    const handleUpdateShow = () => setShowUpdate(true)
    const handleUpdateHide = () => setShowUpdate(false)

    const initialFormState = { id: '', productionLinetype: '', productionLinebrand: '', productionLinemodel: '', productionLinelocation: '' }
    const [currentProductionLine, setCurrentProductionLine] = useState(initialFormState)

    const getAllProductionLines = async () => {
        const allProductionLines = await axios(config.routes.productionLines.getAll);
        const allProductionLineTypes = await axios(config.routes.productionLinetypes.getAll);
        setProductionLines(allProductionLines.data);
        setProductionLineTypes(allProductionLineTypes.data);
        setIsLoading(false);
    }

    const getAllProductionLinesFilterByProductionLineType = async productionLineType => {
        const productionLineTypeId = productionLineType.target.value
        const filterProductionLines = await axios(config.routes.productionLines.filterByProductionLineType + productionLineTypeId);
        const allProductionLineTypes = await axios(config.routes.productionLinetypes.getAll);
        setProductionLines(filterProductionLines.data);
        setProductionLineTypes(allProductionLineTypes.data);
    }

    const createProductionLine = async productionLine => {
        await axios({
            method: "post",
            url: config.routes.productionLines.create,
            data: productionLine,
            headers: { "Content-Type": "application/json;charset=UTF-8" },
        }).catch((error) => {
            console.log(error)
        })
        getAllProductionLines()
    }

    const updateProductionLine = async productionLine => {
        await axios({
            method: "put",
            url: config.routes.productionLines.updateProductionLineType + productionLine.id,
            data: '"' + productionLine.productionLinetype + '"',
            headers: { "Content-Type": "application/json;charset=UTF-8" },
        }).catch((error) => {
            console.log(error)
        })
        getAllProductionLines()
    }

    const editProductionLineRow = productionLine => {
        setCurrentProductionLine(productionLine)
    }

    useEffect(() => {
        getAllProductionLines()
    }, []);

    return (
        <div className="container">

            <h1>ProductionLine</h1>
            {isLoading ?
                <Loading />
                : (
                    <>
                        <ProductionLineNavBar
                            productionLineTypes={productionLineTypes}
                            reload={getAllProductionLines}
                            showCreate={handleCreateShow}
                            filterByProductionLineType={getAllProductionLinesFilterByProductionLineType}
                        />
                        <ProductionLineTable
                            productionLines={productionLines}
                            productionLineTypes={productionLineTypes}
                            editProductionLineRow={editProductionLineRow}
                            showUpdate={handleUpdateShow} />

                        <Modal
                            show={showCreate}
                            onHide={handleCreateHide}
                            size="lg"
                            aria-labelledby="contained-modal-title-vcenter"
                            centered>
                            <Modal.Body>
                                <CreateProductionLineForm
                                    productionLineTypes={productionLineTypes}
                                    createProductionLine={createProductionLine}
                                    hideCreate={handleCreateHide} />
                            </Modal.Body>
                        </Modal>

                        <Modal
                            show={showUpdate}
                            onHide={handleUpdateHide}
                            size="lg"
                            aria-labelledby="contained-modal-title-vcenter"
                            centered>

                            <Modal.Body>
                                <UpdateProductionLineForm
                                    currentProductionLine={currentProductionLine}
                                    productionLineTypes={productionLineTypes}
                                    updateProductionLine={updateProductionLine}
                                    hideUpdate={handleUpdateHide} />
                            </Modal.Body>
                        </Modal>
                    </>
                )}
        </div>
    )
}

export default ProductionLine;