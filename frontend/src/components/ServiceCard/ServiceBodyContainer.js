import React from 'react'
import ServiceBody from './ServiceBody'
import { useState, useEffect } from 'react'

import useToken from '../../useToken'

const ServiceBodyContainer = (props) => {
    const [serviceBody, setServiceBody] = useState([])
    const [serviceEntries, setServiceEntries] = useState([])
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

            // if body.success is not true, then the user is not logged in
            if (!body.success) {
                
                return;
            }
            
            console.log(body.data[0].services)

            // declare an array to store the data
            const serviceEntries = []
            
            const allServices = body.data[0].services

            // loop through the services and push the data into the array
            for (let i = 0; i < allServices.length; i++) {
                const name = allServices[i].name
                const entries = allServices[i].entries

                for (let j = 0; j < entries.length; j++) {
                    serviceEntries.push({
                        id: entries[j].id,
                        name: name,
                        time: entries[j].time,
                        date: entries[j].date,
                    })

                }
            }

            console.log("serviceEntries: ", serviceEntries)
            setServiceEntries(serviceEntries)
            setServiceBody(allServices)



        }

        fetchData()
    }, [])

    return (
        <>

        {props.showEntries ?
        <>
            {serviceEntries.map(service => {
                return (
                    <ServiceBody
                        //key={service.id}
                        id={service.id}
                        name={service.name}
                        plaque={service.plaque}
                        lastUpdate={service.time}
                        status={service.date}

                        showEntries={props.showEntries}
                    />
                )
            })}
        </>
        :
        <>      
            {serviceBody.map(service => {
                return (
                    <ServiceBody
                        //key={service.id}
                        id={service.id}
                        name={service.name}
                        plaque={service.plaque}

                        showEntries={props.showEntries}
                    />
                )
            })}
        </>
        }
        </>
        
    )
}

export default ServiceBodyContainer
