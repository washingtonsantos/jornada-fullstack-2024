INSERT INTO categorias (
    id, 
    nome, 
    descricao, 
    tipo_categoria, 
    icon, 
    color, 
    is_default, 
    ativo, 
    criado_em
) VALUES
-- RECEITAS
('D2AFC928-38E8-4D8B-8C40-000000000001', 'Salário', 'Recebimento de salário', 'Receita', 'wallet', '#4CAF50', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000002', 'Freelance', 'Trabalhos extras', 'Receita', 'briefcase', '#2E7D32', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000003', 'Investimentos', 'Rendimentos de investimentos', 'Receita', 'trending_up', '#388E3C', TRUE, TRUE, '2025-01-01 00:00:00+00'),

-- DESPESAS
('D2AFC928-38E8-4D8B-8C40-000000000101', 'Moradia', 'Aluguel, condomínio, IPTU', 'Despesa', 'home', '#F44336', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000102', 'Alimentação', 'Mercado e restaurantes', 'Despesa', 'restaurant', '#FF9800', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000103', 'Transporte', 'Combustível, ônibus, Uber', 'Despesa', 'directions_car', '#2196F3', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000104', 'Saúde', 'Plano de saúde, consultas e medicamentos', 'Despesa', 'favorite', '#E91E63', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000105', 'Educação', 'Cursos e mensalidades', 'Despesa', 'school', '#3F51B5', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000106', 'Lazer', 'Cinema, viagens e entretenimento', 'Despesa', 'sports_esports', '#9C27B0', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000107', 'Compras', 'Roupas e compras em geral', 'Despesa', 'shopping_cart', '#795548', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000108', 'Assinaturas', 'Netflix, Spotify e outros serviços', 'Despesa', 'subscriptions', '#607D8B', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000109', 'Impostos', 'Tributos e taxas', 'Despesa', 'receipt_long', '#B71C1C', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000110', 'Outros', 'Outras despesas', 'Despesa', 'Categoria', '#757575', TRUE, TRUE, '2025-01-01 00:00:00+00'),
('D2AFC928-38E8-4D8B-8C40-000000000111', 'Pet', 'Despesas com Pet', 'Despesa', 'Categoria', '#757575', TRUE, TRUE, '2025-01-01 00:00:00+00')
ON CONFLICT (id) DO NOTHING;

INSERT INTO contas (
    id, 
    nome, 
    tipo_conta, 
    saldo, 
    limite, 
    ativo, 
    criado_em,
    usuario_id
) VALUES
('D2AFC928-38E8-4D8B-8C40-000000000201', 'Banco Itaú', 'ContaCorrente', 0.00, 0.00, TRUE, '2025-01-01 00:00:00+00', '9CE1F444-E7D3-4EEC-80E9-3644A529EB5F'),
('D2AFC928-38E8-4D8B-8C40-000000000202', 'Banco NuBank', 'ContaCorrente', 0.00, 0.00, TRUE, '2025-01-01 00:00:00+00', '9CE1F444-E7D3-4EEC-80E9-3644A529EB5F'),
('D2AFC928-38E8-4D8B-8C40-000000000203', 'Banco Pan', 'ContaCorrente', 0.00, 0.00, TRUE, '2025-01-01 00:00:00+00', '9CE1F444-E7D3-4EEC-80E9-3644A529EB5F'),
('D2AFC928-38E8-4D8B-8C40-000000000204', 'NuBank Ultravioleta', 'CartaoDeCredito', 0.00, 0.00, TRUE, '2025-01-01 00:00:00+00', '9CE1F444-E7D3-4EEC-80E9-3644A529EB5F'),
('D2AFC928-38E8-4D8B-8C40-000000000205', 'Itaú Gold', 'CartaoDeCredito', 0.00, 0.00, TRUE, '2025-01-01 00:00:00+00', '9CE1F444-E7D3-4EEC-80E9-3644A529EB5F'),
('D2AFC928-38E8-4D8B-8C40-000000000206', 'Itaú Gold Adicional', 'CartaoDeCredito', 0.00, 0.00, TRUE, '2025-01-01 00:00:00+00', '9CE1F444-E7D3-4EEC-80E9-3644A529EB5F');