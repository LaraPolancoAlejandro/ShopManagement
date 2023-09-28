# ShopManagement API

## Documentation:
{
    "openapi": "3.0.1",
    "info": {
        "title": "Mi API",
        "version": "v1"
    },
    "paths": {
        "/api/Employees": {
            "get": {
                "tags": [
                    "Employees"
                ],
                "parameters": [
                    {
                        "name": "page",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32",
                            "default": 1
                        }
                    },
                    {
                        "name": "limit",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32",
                            "default": 10
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/Employee"
                                    }
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/Employee"
                                    }
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/Employee"
                                    }
                                }
                            }
                        }
                    }
                }
            },
            "post": {
                "tags": [
                    "Employees"
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Employee"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Employee"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/Employee"
                            }
                        }
                    }
                },
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "$ref": "#/components/schemas/Employee"
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Employee"
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Employee"
                                }
                            }
                        }
                    }
                }
            }
        },
        "/api/Employees/{id}": {
            "get": {
                "tags": [
                    "Employees"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "$ref": "#/components/schemas/Employee"
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Employee"
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Employee"
                                }
                            }
                        }
                    }
                }
            },
            "put": {
                "tags": [
                    "Employees"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Employee"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Employee"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/Employee"
                            }
                        }
                    }
                },
                "responses": {
                    "200": {
                        "description": "Success"
                    }
                }
            },
            "delete": {
                "tags": [
                    "Employees"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success"
                    }
                }
            }
        },
        "/api/Inventory": {
            "get": {
                "tags": [
                    "Inventories"
                ],
                "parameters": [
                    {
                        "name": "page",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32",
                            "default": 1
                        }
                    },
                    {
                        "name": "limit",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32",
                            "default": 10
                        }
                    },
                    {
                        "name": "storeNames",
                        "in": "query",
                        "schema": {
                            "type": "array",
                            "items": {
                                "type": "string"
                            }
                        }
                    },
                    {
                        "name": "flavors",
                        "in": "query",
                        "schema": {
                            "type": "array",
                            "items": {
                                "type": "string"
                            }
                        }
                    },
                    {
                        "name": "minQuantity",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    },
                    {
                        "name": "maxQuantity",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    },
                    {
                        "name": "minDate",
                        "in": "query",
                        "schema": {
                            "type": "string"
                        }
                    },
                    {
                        "name": "maxDate",
                        "in": "query",
                        "schema": {
                            "type": "string"
                        }
                    },
                    {
                        "name": "isSeasonFlavor",
                        "in": "query",
                        "schema": {
                            "type": "boolean"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/InventoryResponseDto"
                                    }
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/InventoryResponseDto"
                                    }
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/InventoryResponseDto"
                                    }
                                }
                            }
                        }
                    }
                }
            },
            "post": {
                "tags": [
                    "Inventories"
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Inventory"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Inventory"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/Inventory"
                            }
                        }
                    }
                },
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "$ref": "#/components/schemas/Inventory"
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Inventory"
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Inventory"
                                }
                            }
                        }
                    }
                }
            }
        },
        "/api/Inventory/{id}": {
            "get": {
                "tags": [
                    "Inventories"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "$ref": "#/components/schemas/InventoryResponseDto"
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/InventoryResponseDto"
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/InventoryResponseDto"
                                }
                            }
                        }
                    }
                }
            },
            "put": {
                "tags": [
                    "Inventories"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Inventory"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Inventory"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/Inventory"
                            }
                        }
                    }
                },
                "responses": {
                    "200": {
                        "description": "Success"
                    }
                }
            },
            "delete": {
                "tags": [
                    "Inventories"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success"
                    }
                }
            }
        },
        "/api/Inventory/upload": {
            "post": {
                "tags": [
                    "Inventories"
                ],
                "requestBody": {
                    "content": {
                        "multipart/form-data": {
                            "schema": {
                                "type": "object",
                                "properties": {
                                    "csvFile": {
                                        "type": "string",
                                        "format": "binary"
                                    }
                                }
                            },
                            "encoding": {
                                "csvFile": {
                                    "style": "form"
                                }
                            }
                        }
                    }
                },
                "responses": {
                    "200": {
                        "description": "Success"
                    }
                }
            }
        },
        "/api/Stores": {
            "get": {
                "tags": [
                    "Stores"
                ],
                "parameters": [
                    {
                        "name": "page",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32",
                            "default": 1
                        }
                    },
                    {
                        "name": "Limit",
                        "in": "query",
                        "schema": {
                            "type": "integer",
                            "format": "int32",
                            "default": 10
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/Store"
                                    }
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/Store"
                                    }
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "type": "array",
                                    "items": {
                                        "$ref": "#/components/schemas/Store"
                                    }
                                }
                            }
                        }
                    }
                }
            },
            "post": {
                "tags": [
                    "Stores"
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Store"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Store"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/Store"
                            }
                        }
                    }
                },
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "$ref": "#/components/schemas/Store"
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Store"
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Store"
                                }
                            }
                        }
                    }
                }
            }
        },
        "/api/Stores/{id}": {
            "get": {
                "tags": [
                    "Stores"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success",
                        "content": {
                            "text/plain": {
                                "schema": {
                                    "$ref": "#/components/schemas/Store"
                                }
                            },
                            "application/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Store"
                                }
                            },
                            "text/json": {
                                "schema": {
                                    "$ref": "#/components/schemas/Store"
                                }
                            }
                        }
                    }
                }
            },
            "put": {
                "tags": [
                    "Stores"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "requestBody": {
                    "content": {
                        "application/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Store"
                            }
                        },
                        "text/json": {
                            "schema": {
                                "$ref": "#/components/schemas/Store"
                            }
                        },
                        "application/*+json": {
                            "schema": {
                                "$ref": "#/components/schemas/Store"
                            }
                        }
                    }
                },
                "responses": {
                    "200": {
                        "description": "Success"
                    }
                }
            },
            "delete": {
                "tags": [
                    "Stores"
                ],
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": true,
                        "schema": {
                            "type": "integer",
                            "format": "int32"
                        }
                    }
                ],
                "responses": {
                    "200": {
                        "description": "Success"
                    }
                }
            }
        }
    },
    "components": {
        "schemas": {
            "Employee": {
                "type": "object",
                "properties": {
                    "id": {
                        "type": "integer",
                        "format": "int32"
                    },
                    "name": {
                        "type": "string",
                        "nullable": true
                    }
                },
                "additionalProperties": false
            },
            "Inventory": {
                "type": "object",
                "properties": {
                    "id": {
                        "type": "integer",
                        "format": "int32"
                    },
                    "storeId": {
                        "type": "integer",
                        "format": "int32",
                        "nullable": true
                    },
                    "employeeId": {
                        "type": "integer",
                        "format": "int32",
                        "nullable": true
                    },
                    "date": {
                        "type": "string",
                        "format": "date-time"
                    },
                    "flavor": {
                        "type": "string",
                        "nullable": true
                    },
                    "isSeasonFlavor": {
                        "type": "boolean"
                    },
                    "quantity": {
                        "type": "integer",
                        "format": "int32"
                    },
                    "employee": {
                        "$ref": "#/components/schemas/Employee"
                    },
                    "store": {
                        "$ref": "#/components/schemas/Store"
                    }
                },
                "additionalProperties": false
            },
            "InventoryResponseDto": {
                "type": "object",
                "properties": {
                    "store": {
                        "type": "string",
                        "nullable": true
                    },
                    "date": {
                        "type": "string",
                        "nullable": true
                    },
                    "flavor": {
                        "type": "string",
                        "nullable": true
                    },
                    "isSeasonFlavor": {
                        "type": "string",
                        "nullable": true
                    },
                    "quantity": {
                        "type": "integer",
                        "format": "int32"
                    },
                    "listedBy": {
                        "type": "string",
                        "nullable": true
                    }
                },
                "additionalProperties": false
            },
            "Store": {
                "type": "object",
                "properties": {
                    "id": {
                        "type": "integer",
                        "format": "int32"
                    },
                    "name": {
                        "type": "string",
                        "nullable": true
                    },
                    "inventories": {
                        "type": "array",
                        "items": {
                            "$ref": "#/components/schemas/Inventory"
                        },
                        "nullable": true
                    }
                },
                "additionalProperties": false
            }
        }
    }
}
