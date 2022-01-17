import React from 'react'

const ServiceBody = (props) => {
    return (
        <tbody>
                <tr>
                <th >{props.name}</th>
                <td> {props.plaque} </td>
                <td> {props.lastUpdate} 12:45 </td>
                <td> {props.status} 17/01 </td>
                
                
                </tr>
        </tbody>
    )
}

export default ServiceBody
