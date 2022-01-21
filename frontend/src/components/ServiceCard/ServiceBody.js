import React from 'react'

const ServiceBody = (props) => {
    return (
        <tbody>
                <tr>
                <th >{props.name}</th>
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
