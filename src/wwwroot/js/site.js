const connection = new signalR.HubConnectionBuilder()
    .withUrl("/feedbackHub")
    .build();

connection.on("ReceiveUpdate", sessionId => {
    loadResults(sessionId);
});

async function loadResults(sessionId) {
    const res = await fetch(`/api/admin/sessions/${sessionId}/results`);
    const stats = await res.json();
    // Logik für um ins DOM zu schreiben
}

connection.start().catch(err => console.error(err));

// Beim ersten Laden
loadResults(@Model.SessionId);