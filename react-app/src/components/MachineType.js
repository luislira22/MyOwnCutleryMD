import React, { Component } from 'react'

const MachineTypes = ({ machineTypes }) => {
    return (
        <table className="table">
            <thead>
                <tr>
                    <th scope="col">MachineTypeId</th>
                    <th scope="col">MachineType Description</th>
                </tr>
            </thead>
            <tbody>
                {machineTypes.map((machineType) => (
                    <tr>
                        <td>{machineType.id} </td>
                        <td>{machineType.type} </td>
                    </tr>
                ))}
            </tbody>
        </table>
    )
};

export default MachineTypes;