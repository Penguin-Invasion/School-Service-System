import React from 'react'

import { Button } from "reactstrap";

const showId = (id) => {
    console.log(id)
}

const ServiceBody = (props) => {
    return (
        <tbody>
                <tr>
                <th onClick={() => showId(props.id)} > <Button>{props.name}</Button> </th>
                <td> {props.plaque} </td>
                {
                    props.showEntries ?
                    <>
                    <td> {props.time}</td>
                    <td> {props.date}</td>
                    </>
                    :
                    <>
                    <td> {props.driver}</td>
                    </>
                }
                
                
                </tr>
        </tbody>
    )
}

export default ServiceBody
