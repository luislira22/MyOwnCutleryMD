
import React, { useEffect, useState } from 'react'
import Loading from './Loading'
import { FaSyncAlt, FaPlus, FaPencilAlt, FaSearch, FaCheck } from 'react-icons/fa';
import config from '../Config'
import axios from "axios";
import $ from 'jquery';

function Machines() {
    const [machines, setMachines] = useState([]);
    const [machineTypes, setMachineTypes] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    const fetchData = async () => {
        const allMachines = await axios(config.routes.machines.getAll);
        const allMachineTypes = await axios(config.routes.machinetypes.getAll);
        setMachines(allMachines.data);
        setMachineTypes(allMachineTypes.data);
        setIsLoading(false);
    };

    useEffect(() => {
        fetchData();
    }, []);

    const fetchFilterByMachineType = async (machineTypeId) => {
        const filterMachines = await axios(config.routes.machines.filterByMachineType + machineTypeId);
        const allMachineTypes = await axios(config.routes.machinetypes.getAll);
        setMachines(filterMachines.data);
        setMachineTypes(allMachineTypes.data);
        setIsLoading(false);
    };






    return (
        <>
            <h3>Machines</h3>
            {
                isLoading ? <Loading /> :
                    (
                        <>
                            <NavBar machineTypes={machineTypes} reloadInfo={fetchData} filterByMachineType={fetchFilterByMachineType} />
                            <MachineTable machines={machines} machineTypes={machineTypes} />
                            <CreateMachine machineTypes={machineTypes} reloadInfo={fetchData} />
                        </>
                    )
            }
        </>
    )
}

function NavBar(props) {

    function handleReload() {
        props.reloadInfo();
        $('#machineTypeSelection').prop('selectedIndex', 0);
    }

    function handleFilter(e) {
        let machineTypeId = e.target.value;
        props.filterByMachineType(machineTypeId);
    }

    return (
        <>
            <div class="row justify-content-between">
                <div class="col-6">
                    <button type="button" className="btn btn-outline-primary btn-sm" style={{ margin: "10px 4px 10px 4px" }} onClick={handleReload}>
                        <FaSyncAlt className="react-icons" />
                    </button>
                    <button type="button" className="btn btn-outline-primary btn-sm" style={{ margin: "10px 4px 10px 4px" }} data-toggle="modal" data-target="#createMachineModal">
                        <FaPlus className="react-icons" />
                    </button>
                </div>

                <div class="col-4">
                    <div class="form-check-inline">
                        <FaSearch className="react-icons" />
                    </div>
                    <div class="form-check-inline">

                        <select id="machineTypeSelection" className="form-control" onChange={handleFilter.bind(this)} required>
                            <option selected disabled>Filter by machine type...</option>
                            {props.machineTypes.map(machinetype => (
                                <option key={machinetype.id} value={machinetype.id}>{machinetype.type}</option>
                            ))}
                        </select>
                    </div>
                </div>
            </div>
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
                    <th scope="col">Actions</th>
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

    function ss(e) {
        let row = e.parentElement.nodeName;
        console.log(row);
    }

    return (
        <tr>
            <td>{props.machine.id}</td>
            <td>{machineType ? machineType.type : props.machine.machineType}</td>
            <td>{props.machine.machineBrand}</td>
            <td>{props.machine.machineModel}</td>
            <td>{props.machine.machineLocation}</td>
            <td style={{ textAlign: "center" }}>
                <button id="update" type="button" className="btn btn-outline-primary btn-sm" onClick="">
                    <i><FaPencilAlt className="react-icons" /></i>
                </button>
            </td>
        </tr>
    )
}


function CreateMachine(props) {

    const [createMachineType, setCreateMachineType] = useState("");
    const [createMachineLocation, setCreateMachineLocation] = useState("");
    const [createMachineBrand, setCreateMachineBrand] = useState("");
    const [createMachineModel, setCreateMachineModel] = useState("");

    const handleSubmit = async event => {
        event.preventDefault();

        await postMachine();

        $('#createMachineModal').modal('hide');
        $('#createMachineForm')[0].reset();

        props.reloadInfo();
    }

    const postMachine = async () => {

        const requestHeader = {
            "Content-Type": "application/json;charset=UTF-8",
        }
        const requestBody = {
            machinetype: createMachineType,
            machinelocation: createMachineLocation,
            machinebrand: createMachineBrand,
            machinemodel: createMachineModel
        }

        await axios({
            method: "post",
            url: config.routes.machines.createMachine,
            data: requestBody,
            headers: requestHeader,
        });
    }

    return (
        <>
            <div class="modal fade bd-example-modal-lg" id="createMachineModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                            <h3>Create</h3>
                            <br />
                            <form id="createMachineForm" onSubmit={handleSubmit}>
                                <div className="form-row">
                                    <div className="form-group col-md-6">
                                        <label for="inputState">Machine Type</label>
                                        <select id="inputState" className="form-control" onChange={e => setCreateMachineType(e.target.value)} required>
                                            <option selected disabled>Choose...</option>
                                            {props.machineTypes.map(machinetype => (
                                                <option key={machinetype.id} value={machinetype.id}>{machinetype.type}</option>
                                            ))}
                                        </select>
                                    </div>
                                    <div className="form-group col-md-6">
                                        <label for="machineLocation">Location</label>
                                        <input type="text" className="form-control" id="machineLocation" onChange={e => setCreateMachineLocation(e.target.value)} />
                                    </div>
                                </div>
                                <div className="form-row">
                                    <div className="form-group col-md-6">
                                        <label for="machineBrand">Brand</label>
                                        <input type="text" className="form-control" id="machineBrand" onChange={e => setCreateMachineBrand(e.target.value)} />
                                    </div>
                                    <div className="form-group col-md-6">
                                        <label for="machineModel">Model</label>
                                        <input type="text" className="form-control" id="machineModel" onChange={e => setCreateMachineModel(e.target.value)} />
                                    </div>
                                </div>
                                <button type="submit" className="btn btn-primary">Create</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}


export default Machines;