import React from 'react'
import ServiceBody from './ServiceBody'
import { useState, useEffect } from 'react'

import useToken from '../../useToken'

const ServiceBodyContainer = () => {
    const [serviceBody, setServiceBody] = useState([])
    const { token } = useToken();

    // fetch data from the api
    useEffect(() => {
        const fetchData = async () => {
            const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School', {
                method: 'GET',
                headers: {
                    // set token
                    'Authorization': 'Bearer ' + token,
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
                    plaque={service.plaque}
                    lastUpdate={service.lastUpdate}
                    status={service.status}
                />
            )
        })}
        </>
    )
}

export default ServiceBodyContainer
