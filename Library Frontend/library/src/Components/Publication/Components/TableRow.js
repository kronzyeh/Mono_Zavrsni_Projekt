import React from 'react';

export default function TableRow ({ rowData }) {
    return (
      <tr>
        {rowData.map((cellData, index) => (
          <td key={index}>{cellData}</td>
        ))}
      </tr>
    );
  };
  