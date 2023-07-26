# Dinner API

- [Dinner API](#dinner-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)

## Auth

### Register

```js
    POST {{host}}/auth/register
```

#### Register Request

```json
{
  "firstName": "Mordechai",
  "lastName": "Martinband",
  "email": "moder@gmail.com",
  "password": "abc123!"
}
```

#### Register Response

```js
200 OK
```

```json
{
        "id":"aba50e9f-2417-414b-a518-e9d029df3b43"
        "firstName":"Mordechai",
        "lastName":"Martinband",
        "email":"moder@gmail.com",
        "token":"a20232132"
    }
```

### Login

#### Login Request

```js
    POST {{host}}/auth/login
```

```json
{
  "email": "moder@gmail.com",
  "password": "a20232132"
}
```

#### Login Response

```js
200 OK
```

```json
{
        "id":"aba50e9f-2417-414b-a518-e9d029df3b43"
        "firstName":"Mordechai",
        "lastName":"Martinband",
        "email":"moder@gmail.com",
        "token":"a20232132"
    }
```

