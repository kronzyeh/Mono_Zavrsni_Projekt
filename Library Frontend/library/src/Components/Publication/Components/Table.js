import React from 'react';

class Table extends React.Component{
   render() {
    const { tableName, columnNames, children } = this.props;

    return(
        <div>
        <header> <b>{tableName} </b> </header>
        <table className="user-list">
        <thead>
        <tr>
            {columnNames.map((columnName, index) => (
              <th key={index}>{columnName}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {this.props.children}
        </tbody>
      </table>
      </div>
    );
   }
}

export default Table;