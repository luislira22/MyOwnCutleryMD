import React from 'react'
import { FaSyncAlt, FaPlus, FaPencilAlt, FaSearch, FaCheck } from 'react-icons/fa';
import { Dropdown, DropdownButton } from 'react-bootstrap'

const MachineTypeTable = props => (
    <table className="table">
        <thead>
            <tr>
                <th scope="col">Type</th>
                <th scope="col">Operations</th>
                <th scope="col">Actions</th>
            </tr>
        </thead>
        <tbody>
            {props.machineTypes.map(machineType => (
                <MachineTypeRow
                    machineType={machineType}
                    operations={props.operations}
                    editMachineTypeRow={props.editMachineTypeRow}
                    showUpdate={props.showUpdate} />
            ))}
        </tbody>
    </table>
)

const MachineTypeRow = props => {
    let listOp = []
    props.machineType.operations.forEach(operation => {
        props.operations.find(function (element) {
            if (element.id === operation) {
                listOp.push(element)
            }
        })
    })

    const handleUpdate = event => {
        const newMachineType = {
            id: props.machineType.id,
            type: props.machineType.type == null ? "" : props.machineType.type,
            operation: props.machineType.operation == null ? "" : props.machineType.operation,
        }
        props.editMachineTypeRow(newMachineType)
        props.showUpdate()
    }

    return (
        <tr>
            <td>{props.machineType.type}</td>
            <td>
                <DropdownButton alignCenter id="dropdown-menu-align-right" title="Operations" size="sm">
                    {listOp.map(op => (
                        <Dropdown.Item as="button" key={op.id}>{op.description} - {op.tool}</Dropdown.Item>
                    ))}
                </DropdownButton>
            </td>
            <td style={{ textAlign: "center" }}>
                <button id="update" type="button" className="btn btn-outline-primary btn-sm" onClick={handleUpdate}>
                    <i><FaPencilAlt className="react-icons" /></i>
                </button>
            </td>
        </tr>
    )
}

export default MachineTypeTable;
