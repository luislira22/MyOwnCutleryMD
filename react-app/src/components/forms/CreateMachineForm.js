import React, { useState } from 'react'

const CreateMachineForm = props => {
    const initialFormState = {machinetype: '', brand: '', model: '', location: ''}
    const [machine, setMachine] = useState(initialFormState)

    const handleInputChange = event => {
        const { name, value } = event.target
        setMachine({ ...machine, [name]: value })
    }

    const handleSubmit = event => {
        event.preventDefault()
         if (!machine.machinetype)  return
        props.createMachine(machine)
        setMachine(initialFormState);
        document.getElementById("createForm").reset();
    }
    return (
        <form id="createForm" onSubmit={handleSubmit}>
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
                    <input type="text" className="form-control" name="machinelocation" onChange={handleInputChange} />
                </div>
            </div>
            <div className="form-row">
                <div className="form-group col-md-6">
                    <label for="machineBrand">Brand</label>
                    <input type="text" className="form-control" name="machinebrand" onChange={handleInputChange} />
                </div>
                <div className="form-group col-md-6">
                    <label for="machineModel">Model</label>
                    <input type="text" className="form-control" name="machinemodel" onChange={handleInputChange} />
                </div>
            </div>
            <button type="submit" className="btn btn-primary">Create</button>
        </form>

    )
}

export default CreateMachineForm