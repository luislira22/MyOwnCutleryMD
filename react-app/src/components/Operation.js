import React, { useEffect, useState } from "react"


function Operations() {
    return (
        <GetOperationByID/>
    )
}

function handleCick() {

}

function GetOperationByID() {
    const [Id, SetId] = useState();

    return (
        <div>
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
            </form>
        </div>
    )
}


function PostOperation() {

}

export default Operations