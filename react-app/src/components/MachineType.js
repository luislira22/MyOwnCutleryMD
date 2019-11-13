import React, { useState, useEffect } from 'react'
import axios from "axios";
import Loading from './Loading'
import MachineTypeTable from './tables/MachineType/MachineTypeTable'
import MachineTypeNavBar from './bars/MachineType/MachineTypeNavBar'
import CreateMachineTypeForm from './forms/MachineType/CreateMachineTypeForm'
import UpdateMachineTypeForm from './forms/MachineType/UpdateMachineTypeForm'
import config from '../Config'
import Modal from 'react-bootstrap/Modal';


const MachineType = () => {

    const [machineTypes, setMachineTypes] = useState([])
    const [operations, setOperations] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    const [showCreate, setShowCreate] = useState(false)

    const handleCreateShow = () => setShowCreate(true)
    const handleCreateHide = () => setShowCreate(false)

    const [showUpdate, setShowUpdate] = useState(false)

    const handleUpdateShow = () => setShowUpdate(true)
    const handleUpdateHide = () => setShowUpdate(false)

    const initialFormState = { id: '', type: '', operations: ''}
    const [currentMachineType, setCurrentMachineType] = useState(initialFormState)

    const getAllMachineTypes = async () => {
        const allMachineTypes = await axios(config.routes.machinetypes.getAll);
        const allOperations = await axios(config.routes.operations.getAll);
        setMachineTypes(allMachineTypes.data);
        setOperations(allOperations.data);
        setIsLoading(false);
    }

    const getAllMachineTypesFilterByOperation = async operation => {
        const operationId = operation.target.value
        const filterMachineTypes = await axios(config.routes.machineTypes.filterByOperation + operationId);
        const allOperations = await axios(config.routes.operations.getAll);
        setMachineTypes(filterMachineTypes.data);
        setOperations(allOperations.data);
    }

    const createMachineType = async machineType => {
        await axios({
            method: "post",
            url: config.routes.machinetypes.create,
            data: machineType,
            headers: { "Content-Type": "application/json;charset=UTF-8" },
        }).catch((error) => {
            console.log(error)
        })
        getAllMachineTypes()
    }

    const updateMachineType = async machineType => {
        await axios({
            method: "put",
            url: config.routes.machineTypes.updateOperation + machineType.id,
            data: '"' + machineType.operation + '"',
            headers: { "Content-Type": "application/json;charset=UTF-8" },
        }).catch((error) => {
            console.log(error)
        })
        getAllMachineTypes()
    }

    const editMachineTypeRow = machineType => {
        setCurrentMachineType(machineType)
    }

    useEffect(() => {
        getAllMachineTypes()
    }, []);

    return (
        <div className="container">

            <h1>MachineType</h1>
            {isLoading ?
                <Loading />
                : (
                    <>
                        <MachineTypeNavBar
                            operations={operations}
                            reload={getAllMachineTypes}
                            showCreate={handleCreateShow}
                            filterByOperation={getAllMachineTypesFilterByOperation}
                        />
                        <MachineTypeTable
                            machineTypes={machineTypes}
                            operations={operations}
                            editMachineTypeRow={editMachineTypeRow}
                            showUpdate={handleUpdateShow} />

                        <Modal
                            show={showCreate}
                            onHide={handleCreateHide}
                            size="lg"
                            aria-labelledby="contained-modal-title-vcenter"
                            centered>
                            <Modal.Body>
                                <CreateMachineTypeForm
                                    operations={operations}
                                    createMachineType={createMachineType}
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
                                <UpdateMachineTypeForm
                                    currentMachineType={currentMachineType}
                                    operations={operations}
                                    updateMachineType={updateMachineType}
                                    hideUpdate={handleUpdateHide} />
                            </Modal.Body>
                        </Modal>
                    </>
                )}
        </div>
    )
}

export default MachineType;