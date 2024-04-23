import pandas as pd

base_cols = ['URN', 'Total Internal Floor Area', 'Age Average Score', 'OfstedRating (name)', 'Percentage SEN',
             'Percentage Free school meals', 'Number of pupils']

category_settings = {
    'Teaching and Teaching support staff': {
        'type': 'pupil',
        'outstanding_10': ['amber', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['amber', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'amber', 'red'],
        'other_10': ['red', 'red', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'red', 'red'],
        'other': ['red', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'amber', 'red']
    },
    'Non-educational support staff': {
        'type': 'pupil',
        'outstanding_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red']
    },
    'Educational supplies': {
        'type': 'pupil',
        'outstanding_10': ['amber', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['amber', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'amber', 'red'],
        'other_10': ['red', 'red', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'red', 'red'],
        'other': ['red', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'amber', 'red']
    },
    'IT': {
        'type': 'pupil',
        'outstanding_10': ['amber', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['amber', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'amber', 'red'],
        'other_10': ['red', 'red', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'red', 'red'],
        'other': ['red', 'amber', 'amber', 'green', 'green', 'green', 'amber', 'amber', 'amber', 'red']
    },
    'Premises': {
        'type': 'area',
        'outstanding_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red']
    },
    'Utilities': {
        'type': 'area',
        'outstanding_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red']
    },
    'Administrative supplies': {
        'type': 'pupil',
        'outstanding_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red']
    },
    'Catering staff and supplies': {
        'type': 'pupil',
        'outstanding_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red']
    },
    'Other costs': {
        'type': 'pupil',
        'outstanding_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'outstanding': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other_10': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red'],
        'other': ['green', 'green', 'green', 'amber', 'amber', 'amber', 'amber', 'amber', 'red', 'red']
    }
}


def is_area_close_comparator(y, x):
    return (((x['Total Internal Floor Area'] * 0.9) >= y['Total Internal Floor Area']) | (
                y['Total Internal Floor Area'] <= (x['Total Internal Floor Area'] * 1.1))
            & ((x['Age Average Score'] * 0.75) >= y['Age Average Score']) | (
                        y['Age Average Score'] <= (x['Age Average Score'] * 1.25)))


def is_pupil_close_comparator(y, x):
    return (((x['Number of pupils'] * 0.75) >= y['Number of pupils']) | (
                y['Number of pupils'] <= (x['Number of pupils'] * 1.25))
            & ((x['Percentage Free school meals'] * 0.95) >= y['Percentage Free school meals']) | (
                        y['Percentage Free school meals'] <= (x['Percentage Free school meals'] * 1.05))
            & ((x['Percentage SEN'] * 0.90) >= y['Percentage SEN']) | (
                        y['Percentage SEN'] <= (x['Percentage SEN'] * 1.10))
            )


def is_close_comparator(t, y, x):
    if t == 'area':
        return is_area_close_comparator(y, x)
    else:
        return is_pupil_close_comparator(y, x)


def map_rag(d, ofstead, rag_mapping):
    close_count = d['is_close'][d['is_close']].count()
    key = 'outstanding' if ofstead.lower() == 'outstanding' else 'other'
    key += '_10' if close_count > 10 else ''
    d['rag'] = d['decile'].fillna(0).map(lambda x: rag_mapping[key][int(x)])
    return d


def compute_rag(urn, comparator_set):
    result = {}
    for category in category_settings:
        deciles = category_settings[category]
        cols = (comparator_set.columns.isin(base_cols) | comparator_set.columns.str.startswith(category))

        df = comparator_set[comparator_set.columns[cols]].copy()
        target = comparator_set[comparator_set.index == urn][comparator_set.columns[cols]].copy()

        if deciles['type'] == 'area':
            df['Total'] = df[df.columns[6:df.shape[1]]].sum(axis=1) / (df['Total Internal Floor Area'])
        else:
            df['Total'] = df[df.columns[6:df.shape[1]]].sum(axis=1) / (df['Number of pupils'])

        df['is_close'] = df.apply(lambda x: is_close_comparator(deciles['type'], target, x), axis=1)
        df['decile'] = pd.qcut(df['Total'], 10, labels=False, duplicates='drop')

        result[category] = map_rag(df, target['OfstedRating (name)'].values[0], deciles).sort_values(by='decile',
                                                                                                     ascending=True)
