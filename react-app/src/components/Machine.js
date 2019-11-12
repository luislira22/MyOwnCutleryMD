import React, { useState, useEffect } from 'react'
import axios from "axios";
import Loading from './Loading'
import MachineTable from './tables/Machine/MachineTable'
import MachineNavBar from './bars/Machine/MachineNavBar'
import CreateMachineForm from './forms/Machine/CreateMachineForm'
import UpdateMachineForm from './forms/Machine/UpdateMachineForm'
import config from '../Config'
import Modal from 'react-bootstrap/Modal';


const Machine = () => {

    const [machines, setMachines] = useState([])
    const [machineTypes, setMachineTypes] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    const [showCreate, setShowCreate] = useState(false)
    
    const handleCreateShow = () => setShowCreate(true)
    const handleCreateHide = () => setShowCreate(false)

    const [showUpdate, setShowUpdate] = useState(false)
    
    const handleUpdateShow = () => setShowUpdate(true)
    const handleUpdateHide = () => setShowUpdate(false)

    const initialFormState = { id: '', machinetype: '', machinebrand: '', machinemodel: '', machinelocation: '' }
    const [currentMachine, setCurrentMachine] = useState(initialFormState)

    const getAllMachines = async () => {
        const allMachines = await axios(config.routes.machines.getAll);
        const allMachineTypes = await axios(config.routes.machinetypes.getAll);
        setMachines(allMachines.data);
        setMachineTypes(allMachineTypes.data);
        setIsLoading(false);
    }

    const getAllMachinesFilterByMachineType = async machineType => {
        const machineTypeId = machineType.target.value
        const filterMachines = await axios(config.routes.machines.filterByMachineType + machineTypeId);
        const allMachineTypes = await axios(config.routes.machinetypes.getAll);
        setMachines(filterMachines.data);
        setMachineTypes(allMachineTypes.data);
    }

    const createMachine = async machine => {
        await axios({
            method: "post",
            url: config.routes.machines.create,
            data: machine,
            headers: { "Content-Type": "application/json;charset=UTF-8" },
        }).catch((error) => {
            console.log(error)
        })
        getAllMachines()
    }

    const updateMachine = async machine => {
        await axios({
            method: "put",
            url: config.routes.machines.updateMachineType + machine.id,
            data: '"' + machine.machinetype + '"',
            headers: { "Content-Type": "application/json;charset=UTF-8" },
        }).catch((error) => {
            console.log(error)
        })
        getAllMachines()
    }

    const editMachineRow = machine => {
        setCurrentMachine(machine)
    }

    useEffect(() => {
        getAllMachines()
    }, []);

    return (
        <div className="container">

            <h1>Machine</h1>
            {isLoading ?
                <Loading />
                : (
                    <>
                        <MachineNavBar
                            machineTypes={machineTypes}
                            reload={getAllMachines}
                            showCreate={handleCreateShow}
                            filterByMachineType={getAllMachinesFilterByMachineType}
                        />
                        <MachineTable
                            machines={machines}
                            machineTypes={machineTypes}
                            editMachineRow={editMachineRow}
                            showUpdate={handleUpdateShow} />

                        <Modal 
                            show={showCreate}
                            onHide={handleCreateHide}
                            size="lg"
                            aria-labelledby="contained-modal-title-vcenter"
                            centered>
                            <Modal.Body>
                                <CreateMachineForm
                                    machineTypes={machineTypes}
                                    createMachine={createMachine}
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
                                <UpdateMachineForm
                                    currentMachine={currentMachine}
                                    machineTypes={machineTypes}
                                    updateMachine={updateMachine}
                                    hideUpdate={handleUpdateHide} />
                            </Modal.Body>
                        </Modal>
                    </>
                )}





        </div>
    )
}

export default Machine;