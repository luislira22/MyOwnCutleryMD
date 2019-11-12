import React, { useState, useEffect } from 'react'
import axios from "axios";
import Loading from './Loading'
import MachineTable from './tables/MachineTable'
import CreateMachineForm from './forms/CreateMachineForm'
import UpdateMachineForm from './forms/UpdateMachineForm'
import config from '../Config'


const Machine = () => {

    const [machines, setMachines] = useState([]);
    const [machineTypes, setMachineTypes] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    const [editing, setEditing] = useState(false)
    const [creating, setCreating] = useState(false)

    const initialFormState = { id: '', machinetype: '', machinebrand: '', machinemodel: '', machinelocation: '' }
    const [currentMachine, setCurrentMachine] = useState(initialFormState)

    const loadData = async () => {
        const allMachines = await axios(config.routes.machines.getAll);
        const allMachineTypes = await axios(config.routes.machinetypes.getAll);
        setMachines(allMachines.data);
        setMachineTypes(allMachineTypes.data);
        setIsLoading(false);
    }

    const createMachine = async machine => {
        await axios({
            method: "post",
            url: config.routes.machines.createMachine,
            data: machine,
            headers: { "Content-Type": "application/json;charset=UTF-8" },
        }).catch((error) => {
            console.log(error)
        })
        loadData()
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
        loadData()
    }

    const editMachineRow = machine => {
        setCurrentMachine(machine)
    }

    useEffect(() => {
        loadData()
        setCreating(true)
        setEditing(true)
    }, []);

    return (
        <div className="container">

            <h1>Machine</h1>
            {isLoading ?
                <Loading />
                : (
                    <MachineTable
                        machines={machines}
                        machineTypes={machineTypes}
                        editMachineRow={editMachineRow} />

                )}
            {creating &&
                <CreateMachineForm
                    machineTypes={machineTypes}
                    createMachine={createMachine} />}
            {editing &&
                <UpdateMachineForm
                    currentMachine={currentMachine}
                    machineTypes={machineTypes}
                    updateMachine={updateMachine} />}
        </div>
    )
}

export default Machine;