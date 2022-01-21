import React from 'react'
import ServiceBody from './ServiceBody'
import { useState, useEffect } from 'react'

import useToken from '../../useToken'

const getEntries = async (schoolId, serviceId, token) => {
    const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })

    const body = await result.json()
    //console.log("body? ", body)
    return body.data
}



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
            //console.log("all body? ", body)

            // if body.success is not true, then the user is not logged in
            if (!body.success) {
                
                return;
            }
            
            //console.log("all services",body.data[0].services)

            // declare an array to store the data
            const serviceEntries = []
            
            const allServices = body.data[0].services

            // loop through the services and push the data into the array
            for (let i = 0; i < allServices.length; i++) {
                const name = allServices[i].name
                const plaque = allServices[i].plaque
                const id = allServices[i].id
                const data = await getEntries(body.data[0].id, allServices[i].id, token)
                const driverName = data.driver.name + ' ' + data.driver.surName
                const schoolId = body.data[0].id
                allServices[i].driverName = driverName
                allServices[i].studentCount = data.students.length
                allServices[i].schoolId = schoolId
                
                const entries = data.entries

                for (let j = 0; j < entries.length; j++) {
                    serviceEntries.push({
                        key: entries[j].id,
                        id: id,
                        name: name,
                        plaque: plaque,
                        schoolId: schoolId,
                        time: entries[j].time,
                        date: entries[j].date,
                    })

                }
            }

            // sort serviceEntries by date and time
            serviceEntries.sort((a, b) => {
                // convert date to mm/dd/yyyy
                // split date into array due to '/'
                const dateA = a.date.split('/')
                const dateB = b.date.split('/')
                // split time into array due to ':'
                const timeA = a.time.split(':')
                const timeB = b.time.split(':')

                const valueA = new Date(dateA[2], dateA[1], dateA[0], timeA[0], timeA[1], timeA[2])
                const valueB = new Date(dateB[2], dateB[1], dateB[0], timeB[0], timeB[1], timeB[2])
                
                return valueB - valueA
            })

            

            //console.log("serviceEntries: ", serviceEntries)
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
                        key={service.key}
                        id={service.id}
                        name={service.name}
                        plaque={service.plaque}
                        time={service.time}
                        date={service.date}
                        schoolId={service.schoolId}

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
                        key={service.id}
                        id={service.id}
                        name={service.name}
                        plaque={service.plaque}
                        driver={service.driverName}
                        studentCount={service.studentCount}
                        schoolId={service.schoolId}

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
