import sqlite3
from contextlib import closing

class EasySQL:
    def __init__(self, dbname: str):
        self.db_path = f"{dbname}.db"

    def _execute(self, query: str, params: tuple = (), fetch: bool = False):
        with closing(sqlite3.connect(self.db_path)) as connection:
            with connection:
                cursor = connection.cursor()
                cursor.execute(query, params)
                if fetch:
                    return cursor.fetchall()

    def create_table(self, table_name: str, columns: dict):
        col_defs = [f"{k} {v}" for k, v in columns.items()]
        sql_query = f"CREATE TABLE IF NOT EXISTS {table_name} ({', '.join(col_defs)})"
        self._execute(sql_query)

    def print_table(self, table_name: str):
        rows = self.get_table_values(table_name)
        if not rows:
            print(f"Table '{table_name}' is empty.")
        else:
            for row in rows:
                print(row)

    def get_table_values(self, table_name: str):
        sql_query = f"SELECT * FROM {table_name}"
        return self._execute(sql_query, fetch=True)

    def insert_to_table(self, table_name: str, values: dict):
        cols = ", ".join(values.keys())
        placeholders = ", ".join(["?"] * len(values))
        
        sql_query = f"INSERT INTO {table_name} ({cols}) VALUES ({placeholders})"
        self._execute(sql_query, tuple(values.values()))

    def delete_from_table(self, table_name: str, conditions: dict):
        if not conditions:
            return
            
        where_clauses = [f"{k} = ?" for k in conditions.keys()]
        where_string = " AND ".join(where_clauses)
        
        sql_query = f"DELETE FROM {table_name} WHERE {where_string}"
        self._execute(sql_query, tuple(conditions.values()))
    
    def clear_table(self, table_name: str):
        sql_query = f"DELETE FROM {table_name}"
        self._execute(sql_query)
    
    def delete_table(self, table_name: str):
        sql_query = f"DROP TABLE IF EXISTS {table_name}"
        self._execute(sql_query)
