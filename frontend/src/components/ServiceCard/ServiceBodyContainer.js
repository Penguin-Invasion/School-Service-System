import React from 'react'
import ServiceBody from './ServiceBody'
import { useState, useEffect } from 'react'

const ServiceBodyContainer = () => {
    const [serviceBody, setServiceBody] = useState([])

    // fetch data from the api
    useEffect(() => {
        const fetchData = async () => {
            const tokenString = localStorage.getItem('token');
            const userToken = JSON.parse(tokenString);

            const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School', {
                method: 'GET',
                headers: {
                    // set token
                    'Authorization': 'Bearer ' + userToken,
                    'Content-Type': 'application/json'
                }
            })
            const body = await result.json()
            setServiceBody(body.data[0].services)

        }

        fetchData()
    }, [])



    return (
        <>
        {serviceBody.map(service => {
            return (
                <ServiceBody
                    //key={service.id}
                    id={service.id}
                    name={service.name}
                    entrance={service.plaque}
                    exit={service.exit}
                    diff={service.diff}
                />
            )
        })}
        </>
    )
}

export default ServiceBodyContainer
