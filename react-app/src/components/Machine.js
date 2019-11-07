
import React, { useEffect, useState } from 'react'
import Loading from './Loading'
import { FaPlus, FaPencilAlt } from 'react-icons/fa';
import config from '../Config'
import axios from "axios";

function Machines() {
    const [machines, setMachines] = useState([]);
    const [machineTypes, setMachineTypes] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            const allMachines = await axios(config.routes.machines.getAll);
            const allMachineTypes = await axios(config.routes.machinetypes.getAll);
            setMachines(allMachines.data);
            setMachineTypes(allMachineTypes.data);
            setIsLoading(false);
        };

        fetchData();
    }, []);

    return (
        <>
            <h3>Machines</h3>
            {
                isLoading ? <Loading /> :
                    (
                        <>
                            <MachineTable machines={machines} machineTypes={machineTypes} />
                            <CreateMachine machineTypes={machineTypes} />
                        </>
                    )
            }
        </>
    )
}


function MachineTable(props) {
    return (
        <table className="table">
            <thead>
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Machine Type</th>
                    <th scope="col">Brand</th>
                    <th scope="col">Model</th>
                    <th scope="col">Location</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {props.machines.map(machine => (
                    <MachineTableRow key={machine.id} machine={machine} machineTypes={props.machineTypes} />
                ))}
            </tbody>
        </table>
    )
}

function MachineTableRow(props) {
    const machineType = props.machineTypes.find(function (machineType) {
        if (machineType.id === props.machine.machineType) {
            return machineType;
        }
    });

    return (
        <tr>
            <td>{props.machine.id}</td>
            <td>{machineType ? machineType.type : props.machine.machineType}</td>
            <td>{props.machine.machineBrand}</td>
            <td>{props.machine.machineModel}</td>
            <td>{props.machine.machineLocation}</td>
            <td>
                <button type="button" className="btn btn-outline-primary btn-sm ">
                    <i><FaPencilAlt /></i>
                </button>
            </td>
        </tr>
    )
}


function CreateMachine(props) {

    const [machineType, setMachineType] = useState("");
    const [machineLocation, setMachineLocation] = useState("");
    const [machineBrand, setMachineBrand] = useState("");
    const [machineModel, setMachineModel] = useState("");

    const handleSubmit = event => {
        event.preventDefault();

        const requestHeader = {
            "Content-Type": "application/json;charset=UTF-8",
        }
        const requestBody = {
            machinetype: machineType,
            machinelocation: machineLocation,
            machinebrand: machineBrand,
            machinemodel: machineModel
        }

        axios.post(config.routes.machines.createMachine, requestBody, requestHeader);
        axios({
            method: "post",
            url: config.routes.machines.createMachine,
            data: requestBody,
            headers: requestHeader,
        })
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-row">
                <div className="form-group col-md-6">
                    <label for="inputState">Machine Type</label>
                    <select id="inputState" className="form-control" onChange={e => setMachineType(e.target.value)} required>
                        <option selected disabled>Choose...</option>
                        {props.machineTypes.map(machinetype => (
                            <option key={machinetype.id} value={machinetype.id}>{machinetype.type}</option>
                        ))}
                    </select>
                </div>
                <div className="form-group col-md-6">
                    <label for="machineLocation">Location</label>
                    <input type="text" className="form-control" id="machineLocation" onChange={e => setMachineLocation(e.target.value)} />
                </div>
            </div>
            <div className="form-row">
                <div className="form-group col-md-6">
                    <label for="machineBrand">Brand</label>
                    <input type="text" className="form-control" id="machineBrand" onChange={e => setMachineBrand(e.target.value)} />
                </div>
                <div className="form-group col-md-6">
                    <label for="machineModel">Model</label>
                    <input type="text" className="form-control" id="machineModel" onChange={e => setMachineModel(e.target.value)} />
                </div>
            </div>
            <button type="submit" className="btn btn-primary">Create</button>
        </form>

    )
}

export default Machines;