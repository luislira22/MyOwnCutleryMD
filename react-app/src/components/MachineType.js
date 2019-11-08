
import React, { useEffect, useState } from 'react'
import Loading from './Loading'
import { FaPlus, FaPencilAlt } from 'react-icons/fa';
import config from '../Config'
import axios from "axios";

function MachineTypes() {
    const [MachineTypes, setMachineTypes] = useState([]);
    const [Operations, setOperations] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            const allMachineTypes = await axios(config.routes.machinetypes.getAll);
            //const allOperations = await axios(config.routes.operations.getAll);
            setMachineTypes(allMachineTypes.data);
            //setOperations(allOperations.data);
            setIsLoading(false);
        };

        fetchData();
    }, []);

    return (
        <>
            <h3>MachineTypes</h3>
            {
                isLoading ? <Loading /> :
                    (
                        <>
                            <MachineTypeTable MachineTypes={MachineTypes} Operations={Operations}/>
                            <CreateMachineType Operations={Operations} />
                        </>
                    )
            }
        </>
    )
}

function MachineTypeTable(props) {
    return (
        <table className="table">
            <thead>
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">MachineType</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {props.MachineTypes.map(MachineType => (
                    <MachineTypeTableRow key={MachineType.id} MachineType={MachineType} Operations={props.Operations} />
                ))}
            </tbody>
        </table>
    )
}

function MachineTypeTableRow(props) {
    const MachineType = props.Operations.find(function (MachineType) {
        if (MachineType.id === props.MachineType.MachineType) {
            return MachineType;
        }
    });

    return (
        <tr>
            <td>{props.MachineType.id}</td>
            <td>{props.MachineType.type}</td>
            <td>
                <button type="button" className="btn btn-outline-primary btn-sm ">
                    <i><FaPencilAlt /></i>
                </button>
            </td>
        </tr>
    )
}


function CreateMachineType(props) {

    const [MachineType, setMachineType] = useState("");

    const handleSubmit = event => {
        event.preventDefault();

        const requestHeader = {
            "Content-Type": "application/json;charset=UTF-8",
        }
        const requestBody = {
            MachineType: MachineType
        }

        axios.post(config.routes.MachineTypes.createMachineType, requestBody, requestHeader);
        axios({
            method: "post",
            url: config.routes.MachineTypes.createMachineType,
            data: requestBody,
            headers: requestHeader,
        })
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-row">
            <div className="form-group col-md-6">
                    <label for="machineType">Machine Type</label>
                    <input type="text" className="form-control" id="machineType" onChange={e => setMachineType(e.target.value)} />
            </div>
            </div>
            <button type="submit" className="btn btn-primary">Create</button>
        </form>

    )
}

export default MachineTypes;