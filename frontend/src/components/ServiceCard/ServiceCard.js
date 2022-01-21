import React from 'react'
import { Link } from "react-router-dom";

// service info
import ServiceBodyContainer from "components/ServiceCard/ServiceBodyContainer";


import {
    Button,
    Card,
    CardHeader,
    Table,
    Container,
    Row,
    Col,
  } from "reactstrap";

const renderSeeAll = (show) => {
    if (show === true)
    {
        return  <Button>
                    <Link color="blue" to="/admin/tables">Tüm Servisleri Gör</Link>
                </Button>
    }
}

const showAllServices = (show) => {
    // className="penguin-table-body"
    return show ? <div>
    <Table responsive>
        <ServiceBodyContainer showEntries={show}/>
  </Table>
  </div> : <div>
                <Table responsive>
                    <ServiceBodyContainer showEntries={show}/>
              </Table>
              </div>
} 


const ServiceCard = (props) => {
    return (
        <Row className="mt-5">
          <Col className="mb-5 mb-xl-0" xl="12">
            <Card className="shadow penguin-card">
              <CardHeader className="border-0">
                <Row className="align-items-center">
                <div className="col">
                    {
                        // conditional rendering
                        props.dashboard ?
                        <>
                            <h3 className="mb-0">Servis Giriş Çıkış Saatleri </h3>
                            <p className="text-sm mb-0">Buradan Son Giriş Yapan Servisleri Görebilirsiniz</p>
                        </>
                        :
                        <>
                            <h3 className="mb-0">Tüm Ekli Servisler </h3>
                            <p className="text-sm mb-0">Buradan Sisteminizdeki Tüm Servisleri Görebilirsiniz</p>
                        </>
                    }
                </div>
                <div className="col text-right">
                {renderSeeAll(props.dashboard)}
                </div>
                </Row>
              </CardHeader>
              <div>
              <Table className="align-items-center table-flush penguin-table-head" responsive>
                <thead className="thead-light">
                  <tr>
                    {// conditional rendering
                    props.dashboard ? 
                    <>
                        <th scope="col">Servis İsmi</th>
                        <th scope="col">Plakası</th>
                        <th scope="col">Saat</th>
                        <th scope="col">Gün</th>
                    </> : 
                    <>
                        <th scope="col">Servis İsmi</th>
                        <th scope="col">Plakası</th>
                        <th scope="col">Sürücü</th>
                        {/* <th scope="col">Saat</th>
                        <th scope="col">Gün</th> */}
                    </>
                    
                    }
                  </tr>
                </thead>
                </Table>
              </div>
              
              <div>
                    <Table responsive>
                        <ServiceBodyContainer showEntries={props.dashboard}/>
                </Table>
              </div>
            </Card>
          </Col>
        </Row>
    )
}

export default ServiceCard
