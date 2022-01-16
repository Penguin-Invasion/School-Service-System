import React from 'react'
import ServiceBody from './ServiceBody'
import { useState, useEffect } from 'react'

const ServiceBodyContainer = () => {
    const [serviceBody, setServiceBody] = useState([])

    // fetch data from the api
    useEffect(() => {
        const fetchData = async () => {
            const result = await fetch('http://localhost:3001/services')
            const body = await result.json()
            setServiceBody(body)

        }

        fetchData()
    }, [])



    return (
        <>
        {serviceBody.map(service => {
            return (
                <ServiceBody
                    //key={service.id}
                    name={service.name}
                    entrance={service.entrance}
                    exit={service.exit}
                    diff={service.diff}
                />
            )
        })}
        </>
    )
}

export default ServiceBodyContainer
