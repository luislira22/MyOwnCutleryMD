import React from 'react'
import { FaSyncAlt, FaPlus, FaPencilAlt, FaSearch, FaCheck } from 'react-icons/fa';


const MachineTable = props => (
    <table className="table">
        <thead>
            <tr>
                <th scope="col">Id</th>
                <th scope="col">Machine Type</th>
                <th scope="col">Brand</th>
                <th scope="col">Model</th>
                <th scope="col">Location</th>
                <th scope="col">Actions</th>
            </tr>
        </thead>
        <tbody>
            {props.machines.map(machine => (
                <MachineRow
                    key={machine.id}
                    machine={machine}
                    machineTypes={props.machineTypes}
                    editMachineRow={props.editMachineRow} />
            ))}
        </tbody>
    </table>
)

const MachineRow = props => {
    const machineType = props.machineTypes.find(function (machineType) {
        if (machineType.id === props.machine.machineType) {
            return machineType;
        }
    })

    const handleUpdate = event => {
        const newMachine = {
            id: props.machine.id,
            machinetype: props.machine.machineType,
            machinebrand: props.machine.machineBrand == null ? "" : props.machine.machineBrand,
            machinemodel: props.machine.machineModel == null ? "" : props.machine.machineModel,
            machinelocation: props.machine.machineLocation == null ? "" : props.machine.machineLocation,
        }
        props.editMachineRow(newMachine);
    }

    return (
        <tr>
            <td>{props.machine.id}</td>
            <td>{machineType ? machineType.type : props.machine.machineType}</td>
            <td>{props.machine.machineBrand}</td>
            <td>{props.machine.machineModel}</td>
            <td>{props.machine.machineLocation}</td>
            <td style={{ textAlign: "center" }}>
                <button id="update" type="button" className="btn btn-outline-primary btn-sm" onClick={handleUpdate}>
                    <i><FaPencilAlt className="react-icons" /></i>
                </button>
            </td>
        </tr>
    )
}

export default MachineTable;
