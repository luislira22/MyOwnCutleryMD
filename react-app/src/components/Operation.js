import React, { useState } from "react"
import axios from "axios";
import config from '../Config'

function Operations() {
    return (
        <div>
            <GetOperationByID />
            <PostOperation />
        </div>
    )
}

function GetOperationByID() {
    const [Id, SetId] = useState();
    const [Operation, SetOperation] = useState([]);
    const [HasOperation, SetHasOperation] = useState(false);


    const makeGetRequestById = async () => {
        let operation = await axios(config.routes.operations.getById.concat(Id));
        SetOperation(operation.data);
    };

    const handleCick = () => {
        makeGetRequestById();
        SetHasOperation(true);
    }
    return (
        <>
            <h3>Get Operation By ID</h3>
            <br />
            <form>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="basic-addon1">Id</span>
                    </div>
                    <input type="text" pattern="[A-Za-z0-9]{8}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{12}" class="form-control" placeholder="XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX" onChange={e => SetId(e.target.value)} ></input>
                </div>
                <button onClick={handleCick} type="button" class="btn btn-primary">Get Operation</button>
                <br />
                <br />
                {HasOperation && <OperationResponseRow operation={Operation} />}
            </form>
        </>
    )
}

const OperationResponseRow = (props) => {
    return (
        <table className="table">
            <thead>
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Description</th>
                    <th scope="col">Duration</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>{props.operation.id}</td>
                    <td>{props.operation.description}</td>
                    <td>{props.operation.duration}</td>
                </tr>
            </tbody>
        </table>
    )
};


function PostOperation() {

    const [OperationDescription, setOperationDescription] = useState([]);
    const [OperationDuration, setOperationDuration] = useState([]);


    const handleSubmit = event => {
        event.preventDefault();

        const requestHeader = {
            "Content-Type": "application/json;charset=UTF-8",
        }
        const requestBody = {
            description: OperationDescription,
            duration: OperationDuration
        };

        axios.post(config.routes.operations.createOperation, requestBody, requestHeader);
        axios({
            method: "post",
            url: config.routes.operations.createOperation,
            data: requestBody,
            headers: requestHeader,
        })
    }

    return (
        <>
            <h3>createOperation</h3>
            <br />
            <form onSubmit={handleSubmit}>
                <div className="form-group col-md-6">
                    <label for="OperationDescription">Description</label>
                    <input type="text" className="form-control" id="operationDescription" onChange={e => setOperationDescription(e.target.value)} />
                </div>
                <div className="form-group col-md-6">
                    <label for="OperationDuration">Duration</label>
                    <input type="text" className="form-control" id="operationDuration" onChange={e => setOperationDuration(e.target.value)} />
                </div>
                <button type="submit" className="btn btn-primary">Create</button>
            </form>
        </>
    )
}

export default Operations