import React from 'react'

// import Link
import { Link } from "react-router-dom";

import { Button } from "reactstrap";


const ServiceBody = (props) => {
    return (
        <tbody>
                <tr>
                <th> <Link to={`/admin/service-info/${props.schoolId}/${props.id}`}> <Button> {props.name} </Button> </Link> </th>
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
