import React from 'react'

export default function PageHeader({text, icon})
{
    return (
        <>
            <h3>{icon} {text}</h3>
            <hr />
            <br />
        </>
    );
}