-- =====================================================
-- Nome: sp_atualizar_status_pedido
-- Descrição: Atualiza o status de um pedido com validação de transição
-- Retorno: sucesso (0 ou 1) e mensagem
-- Autor: Felipe Sousa
-- =====================================================
CREATE OR REPLACE FUNCTION sp_atualizar_status_pedido(
    p_numero_pedido VARCHAR(50),
    p_novo_status INTEGER
)
RETURNS TABLE (
    sucesso INTEGER,
    mensagem VARCHAR(500)
)
LANGUAGE plpgsql
AS $$
DECLARE
v_status_atual INTEGER;
BEGIN
    -- Buscar status atual do pedido
SELECT "Status"
INTO v_status_atual
FROM "Pedidos"
WHERE "NumeroPedido" = p_numero_pedido
  AND "Ativo" = TRUE;

-- Pedido não encontrado
IF v_status_atual IS NULL THEN
        RETURN QUERY SELECT 0, 'Pedido nao encontrado'::VARCHAR(500);
RETURN;
END IF;

    -- Já está no status desejado
    IF v_status_atual = p_novo_status THEN
        RETURN QUERY SELECT 0, 'Pedido ja esta neste status'::VARCHAR(500);
RETURN;
END IF;

    -- Não pode cancelar pedido entregue
    IF p_novo_status = 6 AND v_status_atual = 5 THEN
        RETURN QUERY SELECT 0, 'Nao pode cancelar pedido entregue'::VARCHAR(500);
RETURN;
END IF;

    -- Atualizar status
UPDATE "Pedidos"
SET
    "Status" = p_novo_status,
    "AtualizadoEm" = CURRENT_TIMESTAMP
WHERE "NumeroPedido" = p_numero_pedido;

-- Sucesso
RETURN QUERY SELECT 1, 'Status atualizado com sucesso'::VARCHAR(500);

EXCEPTION
    WHEN OTHERS THEN
        RETURN QUERY SELECT 0, 'Erro ao atualizar status'::VARCHAR(500);
END;
$$;
