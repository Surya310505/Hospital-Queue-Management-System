
const API = "http://localhost:5051/api";

async function register() {

    const data = {
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        password: document.getElementById("password").value
    };

    const response = await fetch(`${API}/AuthControllers/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });

    if (response.ok) {
        alert("Registration Successful");
        window.location.href = "login.html";
    }
    else {
        alert("Registration Failed");
    }
}

async function login() {

    const data = {
        email: document.getElementById("loginEmail").value,
        password: document.getElementById("loginPassword").value
    };

    const response = await fetch(`${API}/AuthControllers/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        alert("Invalid Credentials");
        return;
    }

    const result = await response.json();

    localStorage.setItem("token", result.token);

    alert("Login Successful");

    window.location.href = "dashboard.html";
}

function addPatient() {

    const patient = document.getElementById("patientName").value;

    const doctor = document.getElementById("doctorName").value;

    const queueList = document.getElementById("queueList");

    const card = document.createElement("div");

    card.className = "card";

    card.innerHTML = `
        <h3>${patient}</h3>
        <p>Doctor: ${doctor}</p>
    `;

    queueList.appendChild(card);
}



// LOAD QUEUE

async function loadQueue() {

    const response = await fetch(API_URL);

    const data = await response.json();

    console.log(data);

    const queueList = document.getElementById("queueList");

    const patientCount = document.getElementById("patientCount");

    queueList.innerHTML = "";

    // PRIORITIZE EMERGENCY PATIENTS

    data.sort((a, b) => b.isEmergency - a.isEmergency);

    // TOTAL PATIENT COUNT

    patientCount.innerText = data.length;

    data.forEach(patient => {

        const card = document.createElement("div");

        // EMERGENCY STYLE

        if (patient.isEmergency) {
            card.className = "queue-card emergency-card";
        }
        else {
            card.className = "queue-card";
        }

        card.innerHTML = `

            <h3>${patient.patientName}</h3>

            <p><strong>Doctor:</strong> ${patient.doctorName}</p>

            <p><strong>Token:</strong> ${patient.tokenNumber}</p>

            <p>
                <strong>Status:</strong>
                ${patient.isEmergency
                    ? "Emergency Priority"
                    : "General"}
            </p>

        `;

        queueList.appendChild(card);

    });
}



// ADD PATIENT

async function addPatient() {

    const patientname =
        document.getElementById("patientname").value;

    const Name =
        document.getElementById("Name").value;

    const IsEmergency =
        document.getElementById("IsEmergency").checked;

    const patient = {
        patientname,
        Name,
        IsEmergency
    };

    const response = await fetch(API_URL, {

        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify(patient)

    });

    if (response.ok) {

        alert("Patient Added Successfully");

        loadQueue();

        document.getElementById("patientname").value = "";
        document.getElementById("Name").value = "";
        document.getElementById("IsEmergency").checked = false;
    }
    else {
        alert("Failed To Add Patient");
    }
}

// DOCTOR STORAGE

let doctors = [];


// LOAD TOTAL PATIENTS

async function loadPatientCount() {

    const response = await fetch(
        "http://localhost:5051/api/QueueControllers"
    );

    const data = await response.json();

    document.getElementById("patientCount")
        .innerText = data.length;
}


// ADD DOCTOR

function addDoctor() {

    const doctorName =
        document.getElementById("doctorName").value;

    const department =
        document.getElementById("department").value;

    const availability =
        document.getElementById("availability").value;

    const doctor = {
        doctorName,
        department,
        availability
    };

    doctors.push(doctor);

    renderDoctors();

    document.getElementById("doctorName").value = "";
    document.getElementById("department").value = "";
}


// RENDER DOCTORS

function renderDoctors() {

    const doctorList =
        document.getElementById("doctorList");

    doctorList.innerHTML = "";

    doctors.forEach((doctor, index) => {

        const card = document.createElement("div");

        card.className = "doctor-card";

        card.innerHTML = `

            <h3>${doctor.doctorName}</h3>

            <p>${doctor.department}</p>

            <p>
                Status:
                <strong>${doctor.availability}</strong>
            </p>

            <button onclick="toggleAvailability(${index})">
                Toggle Availability
            </button>

            <button onclick="removeDoctor(${index})">
                Remove
            </button>

        `;

        doctorList.appendChild(card);

    });
}


// TOGGLE AVAILABILITY

function toggleAvailability(index) {

    if (doctors[index].availability === "Available") {

        doctors[index].availability = "Busy";
    }
    else {

        doctors[index].availability = "Available";
    }

    renderDoctors();
}


// REMOVE DOCTOR

function removeDoctor(index) {

    doctors.splice(index, 1);

    renderDoctors();
}


// CREATE APPOINTMENT

function createAppointment() {

    const patient =
        document.getElementById("patientAppointment").value;

    const department =
        document.getElementById("appointmentDepartment").value;

    const appointmentList =
        document.getElementById("appointmentList");

    const card = document.createElement("div");

    card.className = "queue-card";

    card.innerHTML = `

        <h3>${patient}</h3>

        <p>
            Appointment Department:
            ${department}
        </p>

    `;

    appointmentList.appendChild(card);

    document.getElementById("patientAppointment").value = "";
}


// AUTO LOAD PATIENT COUNT

window.onload = function () {

    loadPatientCount();
};


