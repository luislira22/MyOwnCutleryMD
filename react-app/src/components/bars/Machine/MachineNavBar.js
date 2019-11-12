import React from 'react'
import { FaSyncAlt, FaPlus, FaSearch } from 'react-icons/fa';

const MachineNavBar = props => {
    return (
        <>
            <div class="row justify-content-between">
                <div class="col-6">
                    <button type="button" className="btn btn-outline-primary btn-sm" style={{ margin: "10px 4px 10px 4px" }} onClick={props.reload}>
                        <FaSyncAlt />
                    </button>
                    <button type="button" className="btn btn-outline-primary btn-sm" style={{ margin: "10px 4px 10px 4px" }} onClick={props.showCreate}>
                        <FaPlus />
                    </button>
                </div>

                <div class="col-6">
                    <div class="form-check-inline">
                        <FaSearch />
                    </div>
                    <div class="form-check-inline">

                        <select id="machineTypeSelection" className="form-control" required onChange={props.filterByMachineType.bind(this)}>
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

export default MachineNavBar