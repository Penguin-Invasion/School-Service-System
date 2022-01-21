import Header from "components/Headers/Header.js";

import { Form, FormGroup, Input, Card, CardBody, Button, Col, Container, CardHeader, Row, Table, Label } from 'reactstrap';

import { useState, useEffect } from "react";

import useToken from '../../useToken'




                

const deleteService = async (schoolId, serviceId, token) => {
    const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })

    const body = await result.json()

    console.log("log?")
}


const ServiceInfo = (props) => {
    const [ serviceName, setServiceName ] = useState('');
    const [ servicePlaque, setServicePlaque ] = useState('');
    const [ driverName, setDriverName] = useState('');
    const [ students, setStudents ] = useState([]);
    const { token } = useToken();

    const [ renderStudent, setRenderStudent ] = useState(false);

    const schoolId = props.match.params.schoolId;
    const serviceId = props.match.params.id;

    const [ buttonValue, setButtonValue ] = useState('Yeni Öğrenci Ekle');

    const [ studentName, setStudentName ] = useState('');
    const [ studentSurname, setStudentSurname ] = useState('');
    const [ studentYear, setStudentYear ] = useState('');


    const getService = async ()  => {
        const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }
        })
    
        const body = await result.json()
    
        if (body.success) {
            setServiceName(body.data.name);
            setServicePlaque(body.data.plaque);
            setDriverName(body.data.driver.name + ' ' + body.data.driver.surName);
            setStudents(body.data.students);
        }
    
    }

    useEffect(() => {
        getService();
    }, [])

    const renderStudentForm = () => {
        setRenderStudent(!renderStudent);
        if (!renderStudent) {
            setButtonValue('Öğrenci Eklemeyi Bitir');
        }
        else {
            setButtonValue('Yeni Öğrenci Ekle');
        }
    }

    const addStudent = async () => {
        const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId + '/Student', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify({
                name: studentName,
                surname: studentSurname,
                year: studentYear
            })
        })

        const body = await result.json()

    }

    const deleteStudent = async (studentId) => {
        console.log("studentId", studentId)
        const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId + '/Student/' + studentId, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            }
        })

        const body = await result.json()
    }






    return (
        <>
        <Header />
      {/* Page content */}    
      <Container className="mt--7  " fluid>
      <Row>
        <Col className="order-xl-1" xl="8">
        <CardHeader className="table-head-color border-0">
        <Row className="align-items-center">
        <Col xs="8">
            <h3 className="mb-0 very-light-color">Servis Bilgileri</h3>
            <p>Bu ekrandan, servis bilgielrine bakabilir, servisi silebilir ve yeni öğrenciler ekleyebilirsiniz.</p>
        </Col>
        <Col className="text-right" xs="4">
            <Button onClick={() => deleteService(schoolId, serviceId, token)} color="warning" size="sm">Servisi Kaldır</Button>
        </Col>
        
        </Row>
        </CardHeader>
        <Card  className="bg-success shadow">
            
        <div className="table-service-info">
        <div>
            <h3 className="mb-0 very-light-color">Servis Adı:</h3>
            <p>{serviceName}</p>
        </div>
        <div>
            <h3 className="mb-0 very-light-color">Servis Plakası:</h3>
            <p> {servicePlaque} </p>
        </div>
        <div>
            <h3 className="mb-0 very-light-color">Sürücü İsmi:</h3>
            <p> {driverName} </p>
        </div>
        

        </div>
    <CardHeader className="table-head-color-student border-0">
        <Row className="align-items-center">
        <Col xs="8">
            <h3 className="mb-0 very-light-color">Öğrenci Bilgileri</h3>
            <p>Öğrenci bilgileri listelenmektedir. Yeni Öğrenci eklemek için butonu kullanabilirsiniz.</p>
        </Col>
        <Col className="text-right" xs="4">
            <Button onClick={renderStudentForm} color="info" size="sm">{ buttonValue }</Button>
        </Col>
        {// contidional rendering
        renderStudent ?
        <>
            <Form>
                <Row className="align-items-center">
                    <Col>
                        <FormGroup>
                        <Label for="studentName">Öğrenci Adı</Label>
                            <Input
                                type="text"
                                name="name"
                                id="name"
                                placeholder="İsim"
                                value={studentName}
                                onChange={(e) => setStudentName(e.target.value)}
                            />
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                        <Label for="studentSurname">Öğrenci Soyadı</Label>
                            <Input
                                type="text"
                                name="lastName"
                                id="lastName"
                                placeholder="Soyisim"
                                value={studentSurname}
                                onChange={(e) => setStudentSurname(e.target.value)}
                            />
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                        <Label for="studentYear">Öğrenci Giriş Yılı</Label>
                            <Input
                                type="email"
                                name="email"
                                id="email"
                                placeholder="Giriş Yılı"
                                value={studentYear}
                                onChange={(e) => setStudentYear(e.target.value)}
                            />
                        </FormGroup>
                    </Col>
                    <Col>
                    <Button onClick={addStudent} className="bg-info" >Kaydet</Button>
                    </Col>
                </Row>
            </Form>
        </> : null}
        
        </Row>
    </CardHeader>
    <Table className="table-content-color-student" borderless>
        <thead>
            <tr>
            <th>Öğrenci İsmi</th>
            <th>Öğrenci Soyismi</th>
            <th>Giriş Yılı</th>
            <th>Öğrenciyi Sil</th>
            </tr>
        </thead>
        <tbody>
            
            {students.map( (student, index) => {
                return (
                    <tr key={index}>
                    <td>{student.name}</td>
                    <td>{student.surName}</td>
                    <td>{student.year}</td>
                    <td><Button onClick={() => deleteStudent(student.id)} color="danger" size="sm">Sil</Button></td>
                    </tr>
                )
            })}
            
        </tbody>
    </Table>
    
        </Card>
        </Col>
        </Row>
    </Container>
        </>
    )
}

export default ServiceInfo
