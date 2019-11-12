import React, { useState, useEffect } from 'react'

const UpdateMachineForm = props => {
    const [machine, setMachine] = useState(props.currentMachine)

    useEffect(
        () => {
            setMachine(props.currentMachine)
        },
        [props]
    )

    const handleInputChange = event => {
        const { name, value } = event.target
        setMachine({ ...machine, [name]: value })
    }

    const handleSubmit = event => {
        event.preventDefault()
        props.updateMachine(machine)
        document.getElementById("updateForm").reset()
    }

    return (
        <form id="updateForm" onSubmit={handleSubmit}>
            <div className="form-row">
                <div className="form-group col-md-6">
                    <label for="inputState">Machine Type</label>
                    <select id="inputState" className="form-control" name="machinetype" onChange={handleInputChange} required>
                        <option selected disabled>Choose...</option>
                        {props.machineTypes.map(machinetype => (
                            <option key={machinetype.id} value={machinetype.id}>{machinetype.type}</option>
                        ))}
                    </select>
                </div>
                <div className="form-group col-md-6">
                    <label for="machineLocation">Location</label>
                    <input type="text" className="form-control" value={machine.machinelocation} name="machinelocation" disabled/>
                </div>
            </div>
            <div className="form-row">
                <div className="form-group col-md-6">
                    <label for="machineBrand">Brand</label>
                    <input type="text" className="form-control" value={machine.machinebrand}  name="machinebrand" disabled/>
                </div>
                <div className="form-group col-md-6">
                    <label for="machineModel">Model</label>
                    <input type="text" className="form-control" value={machine.machinemodel} name="machinemodel" disabled/>
                </div>
            </div>
            <button type="submit" className="btn btn-primary">Update</button>
        </form>
    )
}

export default UpdateMachineForm