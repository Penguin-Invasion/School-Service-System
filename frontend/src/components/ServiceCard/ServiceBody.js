import React from 'react'

const ServiceBody = (props) => {
    return (
        <tbody>
                <tr>
                <th scope="row">{props.name}</th>
                <td> {props.entrance} </td>
                <td> {props.exit} </td>
                <td>
                    <i className="fas fa-arrow-up text-success mr-3" /> {props.diff} 
                </td>
                </tr>
        </tbody>
    )
}

export default ServiceBody
