import React, { useState } from 'react'
import { Dropdown, DropdownButton, Form } from 'react-bootstrap'

const CreateMachineTypeForm = props => {
    const initialFormState = { type: '', operations: [] }
    const [machineType, setMachineType] = useState(initialFormState)
    const [operations, setOperations] = useState([])

    const handleInputChange = event => {
        const { name, value } = event.target
        setMachineType({ ...machineType, [name]: value })
    }

    const handleCheckChange = event => {
        const operationId = event.target.id;
        let newOperations = operations;
        if (newOperations.includes(operationId)) {
            const index = newOperations.indexOf(operationId)
            newOperations.splice(index, 1)
        }
        else {
            newOperations.push(operationId)
        }
        setOperations(newOperations)
    }

    const handleSubmit = event => {
        setMachineType({ ...machineType, operations: operations})
        event.preventDefault()
        console.log(machineType)

        if (!machineType.type || machineType.operations.length == 0) return
        props.createMachineType(machineType)
        setMachineType(initialFormState)
        document.getElementById("createForm").reset()
        props.hideCreate()
    }
    return (
        <>
            <h3>Create</h3>
            <hr />
            <form id="createForm" onSubmit={handleSubmit}>
                <div className="form-row">
                    <div className="form-group col-md-6">
                        <label for="type">MachineType</label>
                        <input type="text" className="form-control" name="type" onChange={handleInputChange} required/>
                    </div>
                    <div className="form-group col-md-6">
                        <label for="inputState">Operations</label>
                        {props.operations.map(op => (
                            <Form>
                                <div className="mb-3">
                                    <Form.Check
                                        onChange={handleCheckChange}
                                        custom
                                        inline
                                        label={op.description + " - " + op.tool}
                                        type='checkbox'
                                        id={op.id}
                                    />
                                </div>
                            </Form>))}
                    </div>
                </div>
                <button type="submit" className="btn btn-primary">Create</button>
            </form>
        </>
    )
}

export default CreateMachineTypeForm